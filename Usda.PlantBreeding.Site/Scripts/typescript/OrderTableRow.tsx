import * as React from 'react';
import * as Dropdown from './DropDown';
import * as OrderModel from './OrderModels';
import * as Moment from 'moment';
import * as AutoCompleteInput from './AutoCompleteInput';
import * as AutoComplete from './AutoCompleteModels';

export interface RowProps {
    Row: OrderModel.OrderProduct;
    SelectOptions: Dropdown.Selection[];
    OnUpdateRow: (orderProduct: OrderModel.OrderProduct) => void;
    OnDeleteRow: (orderProduct: OrderModel.OrderProduct) => void;
    DeleteSelected: boolean;
    genusId: number;
}

export interface State {
    DeleteSelected: boolean;
    Row: OrderModel.OrderProduct;
    Changed: boolean;
    Disabled: boolean;
    OverrideDisabledDateSent: boolean;
}


export class TableRowComponent extends React.Component<RowProps, State> {
    constructor(props: RowProps) {
        super(props);
        let disabled = false
        if (this.props.Row.DateSent != null) disabled = true;
        this.state = {
            DeleteSelected: false,
            Row: this.props.Row,
            Changed: false,
            Disabled: disabled,
            OverrideDisabledDateSent: disabled
        };
    }

    componentWillReceiveProps(nextProps: RowProps) {
        const propRow = this.props.Row;
        const stateRow = { ...this.state.Row };
        const nextRow = nextProps.Row;
        let shouldUpdate = false;
        if (this.state.Changed) {
            if (stateRow.GenotypeVM.Id != nextRow.GenotypeVM.Id) {
                stateRow.GenotypeVM = nextRow.GenotypeVM;
                shouldUpdate = true;
            }

            if (stateRow.MaterialId == propRow.MaterialId && propRow.MaterialId != nextRow.MaterialId) {
                stateRow.MaterialId = nextRow.MaterialId;
                shouldUpdate = true;
            }

            if (shouldUpdate) {
                this.setState({
                    Row: stateRow
                });
            }
        }
        else {
            this.setState({
                Row: nextRow
            });
        }
    }

    OnUpdateRow() {
        const row = this.state.Row;
        this.props.OnUpdateRow(row);
        this.setState({
            Changed: false,
        });
    }

    onBlur = () => {
        if (this.state.Changed) {
            this.OnUpdateRow();
        }
    }

    onDelete = (event: React.FormEvent<HTMLButtonElement>) => {
        this.setState({
            DeleteSelected: true
        });
    }

    onModifyRow = () => {
        const dateSent = (this.state.Row.DateSent != null) ? this.state.Row.DateSent : "";
        if (dateSent == "") {
            this.setState({
                Disabled: false
            })
        } else {
            this.setState({
                OverrideDisabledDateSent: false
            })
        }
    }

    onDeleteConfirmed = (event: React.FormEvent<HTMLButtonElement>) => {
        this.props.OnDeleteRow(this.state.Row);
        this.setState({
            DeleteSelected: false
        });
    }

    onCancel = (event: React.FormEvent<HTMLButtonElement>) => {
        this.setState({
            DeleteSelected: false
        });
    }

    onChangeGenotype = (searchParams: AutoComplete.AutoCompleteSearch) => {
        const row = this.state.Row;
        row.GenotypeName = searchParams.params.textVal;
        row.GenotypeId = searchParams.textKey;
        this.setState({
            Row: row,
            Changed: true
        });
        this.OnUpdateRow();
    }

    onChangeQuantity = (event: React.FormEvent<HTMLInputElement>) => {
        const val = parseInt(event.currentTarget.value);
        const row = this.state.Row;
        row.Quantity = val;
        this.setState({
            Row: row,
            Changed: true
        });
        this.OnUpdateRow();
    }

    onChangeVirusTestedDate = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.VirusTested = val;
        this.setState({
            Row: row,
            Changed: true
        });
        this.OnUpdateRow();
    }

    onChangeDateSent = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.DateSent = val;
        this.setState({
            Row: row,
            Changed: true
        });
        this.OnUpdateRow();
    }

    onChangeNotes = (event: React.FormEvent<HTMLTextAreaElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.Note = val;
        this.setState({
            Row: row,
            Changed: true
        });
        this.OnUpdateRow();
    }

    materialUpdate = (selectedCode: string) => {
        const row = this.state.Row;
        row.MaterialId = parseInt(selectedCode);
        this.setState({
            Row: row,
            Changed: true
        });
        this.OnUpdateRow();
    }

    autoGrow = (oField: HTMLTextAreaElement) => {
        if (oField.scrollHeight > oField.clientHeight) {
            oField.style.height = oField.scrollHeight + "px";
        }
    }

    renderAction() {
        if (this.state.Disabled && this.state.OverrideDisabledDateSent) {
            return (<td className="actions"><button className="button modify" onClick={this.onModifyRow}>Modify Row</button></td>);
        }
        else if (this.state.DeleteSelected) {
            return (
                <td className="actions">
                    <div className="outer">
                        <div className="inner"><button className="button confirm" onClick={this.onDeleteConfirmed}>Confirm</button></div>
                        <div className="inner"><button className="button cancel" onClick={this.onCancel}>Cancel</button></div>
                    </div>
                </td>
            );
        } else {
            return (<td className="actions"><button className="button modify" onClick={this.onDelete}>Delete Row</button></td>);
        }
    }

    render() {
        const notes = (this.state.Row.Note != null) ? this.state.Row.Note : "";
        const dateSent = (this.state.Row.DateSent != null) ? this.state.Row.DateSent : "";
        const dateSentDisabled = this.state.Disabled ? (this.state.OverrideDisabledDateSent) : false;
        const selectList: Dropdown.Props = {
            selections: this.props.SelectOptions,
            label: null,
            resourceCode: "material-type",
            selectedCode: this.props.Row.MaterialId.toString() || null,
            updateSelection: this.materialUpdate,
            inputDisabled: this.state.Disabled
        };

        const searchParams: AutoComplete.GenotypeSearch = {
            kind: "genotype",
            textKey: this.props.Row.GenotypeId,
            params: {
                textVal: this.props.Row.GenotypeName,
                genusId: this.props.genusId
            }
        }

        return (
            <tr className="rows">
                <td className="genotype"><AutoCompleteInput.AutoCompleteInput disabled={this.state.Disabled} searchParams={searchParams} fillText={this.onChangeGenotype} /></td>
                <td className="parents row">{this.state.Row.GenotypeVM.FemaleParent} x {this.state.Row.GenotypeVM.MaleParent}</td>
                <td className="cross-type">{this.state.Row.GenotypeVM.CrossTypeName}</td>
                <td className="quantity"><input disabled={this.state.Disabled} className="form-control" onChange={this.onChangeQuantity} onBlur={this.onBlur} type="number" min="0" value={this.state.Row.Quantity} /></td>
                <td className="material"><Dropdown.Dropdown inputDisabled={this.state.Disabled} {...selectList} /></td>
                <td className="virus-tested-date"><input disabled={this.state.Disabled} className="form-control" onChange={this.onChangeVirusTestedDate} onBlur={this.onBlur} type="date" pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" value={this.state.Row.VirusTested} /></td>
                <td className="date-sent"><input disabled={dateSentDisabled} className="form-control" type="date" pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" onBlur={this.onBlur} onChange={this.onChangeDateSent} value={dateSent}/></td>
                <td className="notes"><textarea disabled={this.state.Disabled} id="notes" className="form-control" onFocus={() => { var elem = document.getElementById("notes"); elem.scrollIntoView(true); }} onChange={this.onChangeNotes} onBlur={this.onBlur} value={notes} /></td>
                {this.renderAction()}
            </tr>
        )
    }
}