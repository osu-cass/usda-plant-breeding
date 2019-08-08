import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as OrderSummary from './OrderSummary';
import * as Models from './OrderModels';

namespace CreateDistributionPage {
    export interface Props {
        GenusId: number;
    }

    export interface State {
        Order: Models.Order;
    }

    export class Container extends React.Component<Props, State> {
        constructor(props: Props) {
            super(props);
            this.state = {
                Order: { GenusId: this.props.GenusId }
            };
        }

        onSaveSuccess(order: Models.Order) {
            location.href = '/Accessions/Distributions/Edit/' + order.Id;
        }
       
        render() {
            return (
                <div className="distribution-page">
                    <OrderSummary.Summary
                        Row={this.state.Order} OnSaveSuccess={(order) => this.onSaveSuccess(order)}
                    />
                </div>
            );
        }
    }
}

export function initCreateDistributionPage(pageVM: CreateDistributionPage.Props) {
    const rootDiv = document.getElementById("orders-page") as HTMLDivElement;
    ReactDOM.render(<CreateDistributionPage.Container GenusId={pageVM.GenusId} />, rootDiv);
}