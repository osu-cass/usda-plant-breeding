import * as React from 'react';
import * as Models from './OrderModels';
import * as Dropdown from './DropDown';
import * as NewRow from './NewRow';
import * as OrderTableRow from './OrderTableRow';
import * as AutoComplete from './AutoCompleteModels';
import * as ApiState from './ApiStateModels';

export interface Props {
    Materials: Models.Material[];
    genusId: number;
    orderId: number;
}

interface State {
    orderProductsResult: ApiState.Resource<Models.OrderProduct[]>;
}
interface OrderSearchParams {
    orderId: number
}

export const SaveRowClient = (params: Models.OrderProduct) => ApiState.post<Models.OrderProduct>("/Accessions/Distributions/SaveOrderProduct", params)
export const GetRowsClient = (params: OrderSearchParams) => ApiState.get<Models.OrderProduct[]>("/Accessions/Distributions/GetOrderProducts", params)
export const DeleteRowClient = (params: Models.OrderProduct) => ApiState.post("/Accessions/Distributions/DeleteOrderProduct", params)

export class Container extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            orderProductsResult: { kind: "loading" }
        };

        this.getOrderProducts();
    }

    //Gets a list of order products from the server based on order id
    getOrderProducts() {
        GetRowsClient({ orderId: this.props.orderId })
            .then(data => this.getOrderProductsSuccess(data))
            .catch(err => this.getOrderProductsError(err));

    }

    //Client success
    getOrderProductsSuccess(results: Models.OrderProduct[]) {
        this.setState({ orderProductsResult: { kind: "success", content: results } });
    }

    //Client error
    getOrderProductsError(err: any) {
        this.setState({ orderProductsResult: { kind: "failure" } });
        console.error(err);
    }

    //Saves an order product to the database
    saveOrderProduct(row: Models.OrderProduct) {
        SaveRowClient(row)
            .then(result => this.saveOrderProductSuccess(result))
            .catch(err => this.saveOrderProductError(err));
    }

    //Creates an order product to the database
    createOrderProduct(row: Models.OrderProduct) {
        SaveRowClient(row)
            .then(result => this.createOrderProductSuccess(result))
            .catch(err => this.saveOrderProductError(err));
    }

    deleteOrderProduct(params: Models.OrderProduct) {
        DeleteRowClient(params)
            .then(() => this.deleteProductSuccess(params))
            .catch(err => this.deleteProductError(err));
    }

    deleteProductSuccess(orderProduct: Models.OrderProduct): void {
        this.contentLoadedPromise().then((products) => {
            const rows = products.slice();
            const index = rows.findIndex(op => op.Id == orderProduct.Id);
            if (index >= 0) {
                rows.splice(index, 1);
                this.setState({
                    orderProductsResult: { kind: "success", content: rows }
                });

            } else {
                console.error("Invalid order product ");
            }
        })

    }

    deleteProductError(err: any) {
        console.error(err);
    }


    async contentLoadedPromise() {
        return new Promise<Models.OrderProduct[]>((resolve) => {
            const opResult = this.state.orderProductsResult;
            if (opResult.kind === "success") {
                resolve(opResult.content);
            }
        });
    }


    //Update the state order product from the client
    saveOrderProductSuccess(result: Models.OrderProduct) {
        this.contentLoadedPromise().then((products) => {
            const rows = products.slice();
            const index = rows.findIndex(op => op.Id == result.Id);
            if (index >= 0) {
                rows[index] = { ...result };
                this.setState({
                    orderProductsResult: { kind: "success", content: rows }
                });

            } else {
                console.error("Invalid order product ");
            }
        })
    }

    //Append an item to the state with the new result from client
    createOrderProductSuccess(result: Models.OrderProduct) {
        this.contentLoadedPromise().then((products) => {
            const rows = products.slice();
            const index = rows.findIndex(op => op.Id == result.Id);
            if (index >= 0) {
                console.error("cannot add an existing row ");

            } else {
                rows.push(result);
                this.setState({
                    orderProductsResult: { kind: "success", content: rows }
                });
            }
        })

    }

    //Client error
    saveOrderProductError(err: any) {
        console.error(err);
    }

    renderDropdown(type: string) {
        let resources = this.props.Materials;
        const resCount = resources.length;
    }

    renderRows(selections: Dropdown.Selection[]) {
        const orderProductResult = this.state.orderProductsResult;
        if (orderProductResult.kind == "reloading" || orderProductResult.kind == "success") {
            return orderProductResult.content.map((r, key) => <OrderTableRow.TableRowComponent Row={r}
                key={r.Id}
                OnDeleteRow={(orderProduct) => this.deleteOrderProduct(orderProduct)}
                OnUpdateRow={(orderProduct) => this.saveOrderProduct(orderProduct)}
                SelectOptions={selections} DeleteSelected={false}
                genusId={this.props.genusId} />);
        } else {
            return null;
        }
    }

    renderNewRow(selections: Dropdown.Selection[]) {
        return <NewRow.NewRowComponent
            OnAddRow={(row) => this.createOrderProduct(row)}
            SelectOptions={selections}
            genusId={this.props.genusId}
            orderId={this.props.orderId} />
    }

    getDropdownSelections(): Dropdown.Selection[] {
        const trial: Dropdown.Selection = {
            label: "Select One",
            order: 0,
            selectionCode: "0",
            disabled: true
        };

        const selections = this.props.Materials.map((m, key) => {
            const selection: Dropdown.Selection = {
                label: m.Name,
                order: key,
                selectionCode: m.Id.toString(),
                disabled: false
            };
            return selection;
        });

        selections.unshift(trial);
        return selections;
    }

    render() {
        const selections = this.getDropdownSelections();
        const rows = this.renderRows(selections);
        const newRow = this.renderNewRow(selections);

        return (
            <div className="table-wrapper">
                <table className="order-table" id="order-table">
                    <thead className="order-table-head">
                        <tr>
                            <th className="genotype">Accession</th>
                            <th className="parents">Parents</th>
                            <th className="cross-type">Cross Type</th>
                            <th className="quantity">Quantity</th>
                            <th className="material">Material</th>
                            <th className="virus-tested-date">Virus Tested Date</th>
                            <th className="date-sent">Date Sent</th>
                            <th className="notes">Comment</th>
                            <th className="actions">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {rows}
                        {newRow}
                    </tbody>
                </table>
            </div>
        )
    }
}