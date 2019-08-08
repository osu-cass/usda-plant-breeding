import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as OrderTable from './OrderTable';
import * as NewRow from './NewRow';
import * as Models from './OrderModels';
import * as Summary from './OrderSummary';
import * as ApiState from './ApiStateModels';
import * as AutoComplete from './AutoCompleteModels';
import * as GrowerSummary from './Grower';

namespace OrderPage {
    export interface OrderPageVM {
        Materials: Models.Material[]; 
        OrderId: number;
    }

    export interface State {
        Order: ApiState.Resource<Models.Order>;
    }

    export const GetOrderClient = (params: { id: number }) => ApiState.get<Models.Order>("/Accessions/Distributions/GetOrder", params)

    export class Container extends React.Component<OrderPageVM, State> {
        constructor(props: OrderPageVM) {
            super(props);
            this.state = {
                Order: {kind:"loading"}
            };
            this.getOrder();

        }

        getOrder() {
            GetOrderClient({ id: this.props.OrderId })
                .then((data) => this.onGetOrderSuccess(data))
                .catch((err) => this.onGetOrderError(err));
        }

        onGetOrderSuccess(order: Models.Order) {
            this.setState({
                Order: { kind: "success", content: order }
            });
        }

        onGetOrderError(err: any) {
            this.setState({
                Order: { kind: "failure" }
            });
            console.error(err);
        }


        saveOrder(order: Models.Order) {
            const orderResource = this.state.Order;
            if (orderResource.kind == "success") {
                this.setState({
                    Order: {kind: "success", content: order}
                });
            }
            
        }

        render() {
            const orderResource = this.state.Order;
            if (orderResource.kind == "success" || orderResource.kind == "reloading") {
                const order = orderResource.content;

                return (
                    <div className="orders-page">
                        <Summary.Summary Row={order} OnSaveSuccess={(order) => this.saveOrder(order)} />
                        <GrowerSummary.Summary {...order.Grower} />
                        <div className="summary location">
                            <div className="mailing-address">
                                <span className="text-label">Address:</span>
                                <span className="text-val">{order.LocationAddress}</span>
                            </div>
                        </div>
                        <OrderTable.Container
                            Materials={this.props.Materials}
                            genusId={order.GenusId}
                            orderId={order.Id}
                        />
                    </div>
                );
            }
            else {
                return <div></div>
            }
        }
    }
}

export function initOrdersPage(pageVM: OrderPage.OrderPageVM) {
    const rootDiv = document.getElementById("orders-page") as HTMLDivElement;
    ReactDOM.render(<OrderPage.Container {...pageVM} />, rootDiv);
}