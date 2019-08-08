import * as React from 'react'
import * as Dropdown from './DropDown'
import * as Models from './OrderModels'
import * as AutoComplete from './AutoCompleteModels'
import * as GenotypeInput from './AutoCompleteInput'
import { post } from "./ApiStateModels";

export interface NewRowProps {
    SelectOptions: Dropdown.Selection[];
    OnAddRow: (params: Models.OrderProduct) => void;
    genusId: number;
    orderId: number;
}

export interface NewRowState {
    Row: Models.OrderProduct | null;
    Changed: boolean;
    Invalid: boolean;
    Saving: boolean;
}

export const AddRowClient = (params: Models.OrderProduct) => post<Models.OrderProduct>("/Accessions/Distributions/SaveOrderProduct", params) 

export class NewRowComponent extends React.Component<NewRowProps, NewRowState> {
    constructor(props: NewRowProps) {
        super(props);

        this.state = {
            Row: { ...{} , OrderId: this.props.orderId},
            Changed: false,
            Invalid: true,
            Saving: false
        };
    }

    resetForm() {
        this.setState({
            Row: { ...{}, OrderId: this.props.orderId },
            Changed: false,
            Invalid: true,
            Saving: false
        });
    }

    OnAddRow() {
        this.setState({
            Saving: true
        });
        AddRowClient(this.state.Row)
            .then(data => this.OnAddRowSuccess(data))
            .then(() => this.resetForm())
            .catch(err => this.OnAddRowError(err));
    }

    OnAddRowSuccess(row: Models.OrderProduct) {
        this.resetForm();
        this.props.OnAddRow(row);
    };

    OnAddRowError(err: any) {
        this.setState({
            Invalid: true,
            Saving: false
        });
    }

    onChangeGenotype = (searchParams: AutoComplete.AutoCompleteSearch) => {
        const row = this.state.Row;
        row.GenotypeName = searchParams.params.textVal;
        row.GenotypeId = searchParams.textKey;
        this.validateObject(row);
    }

    validateObject(row: Models.OrderProduct) {
        if (row.GenotypeName && row.VirusTested && row.Quantity && row.MaterialId) {
            this.setState({
                Row: row,
                Changed: true,
                Invalid: false
            });
        } else {
            this.setState({
                Row: row,
                Changed: true,
                Invalid: true
            });
        }

    }

    onChangeQuantity = (event: React.FormEvent<HTMLInputElement>) => {
        const val = parseInt(event.currentTarget.value);
        const row = this.state.Row;
        row.Quantity = val;
        this.validateObject(row);
    }

    onChangeVirusTestedDate = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.VirusTested = val;
        this.validateObject(row);
    }

    onChangeDateSent = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.DateSent = val;
        this.validateObject(row);
    }

    onChangeNotes = (event: React.FormEvent<HTMLTextAreaElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.Note = val;
        this.validateObject(row);
    }

    materialUpdate = (selectedCode: string) => {
        const row = this.state.Row;
        row.MaterialId = parseInt(selectedCode);
        this.validateObject(row);
    }

    render() {
        const row = this.state.Row;
        const selectedCode = row.MaterialId || "0";
        const genotype = (row.GenotypeId != null) ? row.GenotypeId : "";
        const quantity = (row.Quantity != null) ? row.Quantity : "";
        const virusTestedDate = (row.VirusTested != null) ? row.VirusTested : "";
        const notes = (row.Note != null) ? row.Note : "";
        const dateSent = (row.DateSent != null) ? row.DateSent : "";

        const selectList: Dropdown.Props = {
            selections: this.props.SelectOptions,
            label: null,
            resourceCode: "material-type",
            selectedCode: String(selectedCode),
            updateSelection: this.materialUpdate,
            inputDisabled: false
        };

        const searchParams: AutoComplete.GenotypeSearch = {
            kind: "genotype",
            textKey: this.state.Row.GenotypeId,
            params: {
                textVal: this.state.Row.GenotypeName || "",
                genusId: this.props.genusId
            }
        }

        return (
            <tr>
                <td className="genotype"><GenotypeInput.AutoCompleteInput disabled={false} searchParams={searchParams} fillText={this.onChangeGenotype} /></td>
                <td className="parents">N/A</td>
                <td className="cross-type">N/A</td>
                <td className="quantity"><input className="form-control" onChange={this.onChangeQuantity} type="number" min="0" value={quantity} /></td>
                <td className="material"><Dropdown.Dropdown inputDisabled={false} {...selectList} /></td>
                <td className="virus-tested-date"><input className="form-control" onChange={this.onChangeVirusTestedDate} type="date" value={virusTestedDate.toString()} /></td>
                <td className="date-sent"><input className="form-control" onChange={this.onChangeDateSent} type="date" value={dateSent}/></td>
                <td className="notes"><textarea id="notes" className="form-control" onChange={this.onChangeNotes} value={notes} /></td>
                <td><button className="button modify" disabled={this.state.Invalid || this.state.Saving} onClick={() => this.OnAddRow()} >Add Row</button></td>
            </tr>
        )
    }
}