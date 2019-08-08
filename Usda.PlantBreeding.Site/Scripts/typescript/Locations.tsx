import * as React from 'react'
import * as AutoCompleteInput from './AutoCompleteInput'
import * as AutoComplete from './AutoCompleteModels'
import * as OrderModel from './OrderModels'
import { post } from "./ApiStateModels";

export interface Props {
    Location: OrderModel.Location;
    OnSaveSuccess: (order: OrderModel.Location) => void;
}

export interface State {
    Location: OrderModel.Location;
    Changed: boolean;
    Saving: boolean;
    Valid: boolean;
}

export const SaveLocationClient = (params: OrderModel.Location) => post<OrderModel.Location>("/Admin/Locations/Edit", params)

//TODO: add note box

export class EditComponent extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = {
            Location: { ...this.props.Location },
            Changed: false,
            Saving: false,
            Valid: (this.props.Location.Id > 0) ? true: false
        };
    }

    onSaveSuccess(order: OrderModel.Location) {
        this.props.OnSaveSuccess(order);
        this.setState({
            Saving: false,
            Valid: true
        });
    }

    onSaveError(err: any) {
        this.setState({
            Saving: false,
            Valid: false
        });
        console.log(err);
    }

    onSave() {
        this.setState({
            Saving: true
        });
        SaveLocationClient(this.state.Location)
            .then((data) => this.onSaveSuccess(data))
            .catch((err) => this.onSaveError(err));
    }

    onUpdateAll(location: OrderModel.Location) {
        const valid = (location.PrimaryContactId != undefined && location.Description != undefined);
        this.setState({
            Location: { ...location },
            Changed: true,
            Valid: valid
        });
    }

    onChangeGrower = (searchParams: AutoComplete.AutoCompleteSearch) => {
        const location = { ...this.state.Location };
        location.PrimaryContactName = searchParams.params.textVal;
        location.PrimaryContactId = searchParams.textKey;
        this.onUpdateAll(location);
    }

    onChangeName = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const location = this.state.Location;
        location.Name = (val == "") ? undefined: val;
        this.onUpdateAll(location);
    }

 
    onChangeDescription = (event: React.FormEvent<HTMLTextAreaElement>) => {
        const val = event.currentTarget.value;
        const location = this.state.Location;
        location.Description = (val == "") ? undefined: val;
        this.onUpdateAll(location);
    }

    onChangeAddress = (event: React.FormEvent<HTMLTextAreaElement>) => {
        const val = event.currentTarget.value;
        const location = this.state.Location;
        location.Address = (val == "") ? undefined : val;
        this.onUpdateAll(location);
    }

    render() {
        const location = this.state.Location;
        const name = location.Name || "";
        const description = location.Description || "";
        const address = location.Address || "";

        const growerSearchParams: AutoComplete.GrowerSearch = {
            kind: "grower",
            textKey: this.props.Location.PrimaryContactId,
            params: {
                textVal: this.props.Location.PrimaryContactName || ""
            }
        }

        return (
            <div className="summary">
                <div className="name">Name<input className="form-control" onChange={this.onChangeName} value={name}  /></div>
                <div className="grower">Contact<AutoCompleteInput.AutoCompleteInput disabled={false} searchParams={growerSearchParams} fillText={this.onChangeGrower} /></div>
                <div className="description">Code<textarea id="notes" className="form-control" onChange={this.onChangeDescription} value={description} /></div>
                <div className="address">Address<textarea id="address" className="form-control" onChange={this.onChangeAddress} value={address} /></div>
                <div className="create"><button disabled={!this.state.Valid || this.state.Saving} className="button modify" onClick={() => this.onSave()}>Save</button></div>
            </div>
        );
    }
}