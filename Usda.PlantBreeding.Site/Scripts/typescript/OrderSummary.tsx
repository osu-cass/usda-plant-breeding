import * as React from 'react'
import * as AutoCompleteInput from './AutoCompleteInput'
import * as AutoComplete from './AutoCompleteModels'
import * as OrderModel from './OrderModels'
import { post } from "./ApiStateModels";

export interface Props {
    Row: OrderModel.Order;
    OnSaveSuccess: (order: OrderModel.Order) => void;
}

export interface State {
    Row: OrderModel.Order;
    Changed: boolean;
}

export const SaveOrderClient = (params: OrderModel.Order) => post<OrderModel.Order>("/Accessions/Distributions/SaveOrder", params)


export class Summary extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = {
            Row: this.props.Row,
            Changed: false
        };
    }


    componentWillReceiveProps(nextProps: Props) {
        if (nextProps.Row !== this.props.Row) {
            this.setState({
                Row: nextProps.Row
            })
        }
    }

    onSaveSuccess(order: OrderModel.Order) {
        this.props.OnSaveSuccess(order);
        //location.href = '/Accessions/Orders';
    }

    onSaveError(err: any) {
        //TODO: implement this function
        console.log(err);
    }

    onAddRow() {
        SaveOrderClient(this.state.Row)
            .then((data) => this.onSaveSuccess(data))
            .catch((err) => this.onSaveError(err));
    }

    onChangeGrower = (searchParams: AutoComplete.AutoCompleteSearch) => {
        const row = this.state.Row;
        row.GrowerName = searchParams.params.textVal;
        row.GrowerId = searchParams.textKey;
        this.setState({
            Row: row,
            Changed: true
        });
    }

    onChangeLocation = (searchParams: AutoComplete.AutoCompleteSearch) => {
        const row = this.state.Row;
        row.LocationName = searchParams.params.textVal;
        row.LocationId = searchParams.textKey;
        this.setState({
            Row: row,
            Changed: true
        });
    }

    onChangeYear = (event: React.FormEvent<HTMLInputElement>) => {
        const val = parseInt(event.currentTarget.value);
        const row = this.state.Row;
        row.Year = val;
        this.setState({
            Row: row,
            Changed: true
        });
    }

    onChangeName = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.Name = val;
        this.setState({
            Row: row,
            Changed: true
        });
    }

    onChangeNotes = (event: React.FormEvent<HTMLTextAreaElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.Note = val;
        this.setState({
            Row: row,
            Changed: true
        });
    }

    onChangeMTARequestedProp = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.checked;
        const row = this.state.Row;
        row.MTARequestedProp = val;
        this.setState({
            Row: row,
            Changed: true
        });
    }

    onChangeMTARequestedTest = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.checked;
        const row = this.state.Row;
        row.MTARequestedTest = val;
        this.setState({
            Row: row,
            Changed: true
        });
    }

    onChangeMTAComplete = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.MTAComplete = val;
        this.setState({
            Row: row,
            Changed: true
        });
    }


    render() {
        const locationSearchParams: AutoComplete.LocationSearch = {
            kind: "location",
            textKey: this.props.Row.LocationId,
            params: {
                textVal: this.props.Row.LocationName,
                mapFlag: false
            }
        }

        return (
            <div className="summary">
                <div className="name">Name<input className="form-control" onChange={this.onChangeName} value={this.props.Row.Name} /></div>
                <div className="year">Year<input className="form-control" onChange={this.onChangeYear} value={this.props.Row.Year} /></div>
                <div className="location">Location<AutoCompleteInput.AutoCompleteInput disabled={false} searchParams={locationSearchParams} fillText={this.onChangeLocation} /></div>
                <div className="notes">Notes<textarea id="notes" className="form-control" onChange={this.onChangeNotes} value={this.props.Row.Note} /></div>
                <div className="mta-requested-prop">Prop<input type="checkbox" className="form-control" onChange={this.onChangeMTARequestedProp} checked={this.props.Row.MTARequestedProp}/></div>
                <div className="mta-requested-test">Test<input type="checkbox" className="form-control" onChange={this.onChangeMTARequestedTest} checked={this.props.Row.MTARequestedTest} /></div>
                <div className="mta-complete">MTA Complete<input className="form-control" type="date" pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" onChange={this.onChangeMTAComplete} value={this.props.Row.MTAComplete} /></div>
                <div className="create"><button className="button modify" onClick={() => this.onAddRow()}>Save</button></div>
            </div>
        );
    }
}