import * as React from 'react'
import * as Dropdown from './DropDown'
import * as Models from './OrderModels'
import * as AutoComplete from './AutoCompleteModels'
import * as GenotypeInput from './AutoCompleteInput'
import { post } from "./ApiStateModels";
import { MapComponentSummaryVM } from "./Models";

export interface SeedlingMapInfo {
    MapId: number;
    GenusId: number;
    MapYearId: number;
}

export interface SeedlingViewModel extends SeedlingMapInfo {
    CreatedDate?: string;
    GenotypeId?: number;
    GenotypeName?: string;
    PlantNum?: number;
    RowNum?: number;
}


export interface Props {
    MapInfo: SeedlingMapInfo;
    OnAddRow: (params: MapComponentSummaryVM) => void;
}

export interface State {
    Row?: SeedlingViewModel;
    Changed: boolean;
    Invalid: boolean;
    Saving: boolean;
}

export const AddRowClient = (params: SeedlingViewModel) => post<MapComponentSummaryVM>("/Maps/PhenotypeEntry/CreateSeedlingRow", params) 


export class SeedlingRow extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            Row: this.props.MapInfo,
            Changed: false,
            Invalid: true,
            Saving: false
        };
    }

    resetForm() {
        this.setState({
            Row: this.props.MapInfo,
            Changed: false,
            Invalid: false,
            Saving: false
        });
    }

    OnAddRow() {
        this.setState({
            Saving: true
        });
        AddRowClient(this.state.Row)
            .then((data) => this.OnSaveSuccess(data))
            .catch((err) => this.onSaveError(err));
    }

    OnSaveSuccess(mapComponent: MapComponentSummaryVM) {
        this.resetForm();
        this.props.OnAddRow(mapComponent);
    }

    onSaveError(err: any) {
        this.setState({
            Saving: false,
            Invalid: true
        });
        console.log(err);

    }

    onChangeGenotype = (searchParams: AutoComplete.AutoCompleteSearch) => {
        const row = this.state.Row;
        row.GenotypeName = searchParams.params.textVal;
        row.GenotypeId = searchParams.textKey;
        this.validateRow(row);
    }

  
    validateRow(row: SeedlingViewModel): void{
        if (row.CreatedDate && row.GenotypeId && row.PlantNum && row.RowNum) {
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

    onChangePlantNum = (event: React.FormEvent<HTMLInputElement>) => {
        const val = parseInt(event.currentTarget.value);
        const row = this.state.Row;
        row.PlantNum = val;
        this.validateRow(row);
    }

    onChangeRowNum = (event: React.FormEvent<HTMLInputElement>) => {
        const val = parseInt(event.currentTarget.value);
        const row = this.state.Row;
        row.RowNum = val;
        this.validateRow(row);
    }

    onChangeCreatedDate = (event: React.FormEvent<HTMLInputElement>) => {
        const val = event.currentTarget.value;
        const row = this.state.Row;
        row.CreatedDate = val;
        this.validateRow(row);
    }


    render() {
        const row = this.state.Row;
        const genotypeName = row.GenotypeName || "";
        const plantNum = row.PlantNum || "";
        const createdDate = row.CreatedDate || "";
        const rowNum = row.RowNum || "";   
        
        const searchParams: AutoComplete.GenotypeMapSearch = {
            kind: "genotypemap",
            textKey: row.GenotypeId,
            params: {
                textVal: genotypeName,
                genusId: row.GenusId,
                mapId: row.MapId
            }
        }
        return (
            <div className="new-row">
                <div className="genotype"><GenotypeInput.AutoCompleteInput disabled={false} searchParams={searchParams} fillText={this.onChangeGenotype} placeHolder="accession" /></div>
                <div className="row-num"><input className="form-control" onChange={this.onChangeRowNum} type="number" min="1" value={rowNum} placeholder="row"/></div>
                <div className="plant-num"><input className="form-control" onChange={this.onChangePlantNum} type="number" min="0" value={plantNum} placeholder="plot" /></div>
                <div className="created-date"><input className="form-control" required={true} onChange={this.onChangeCreatedDate} type="date" value={createdDate} /></div>
                <div><button className="modify" disabled={this.state.Invalid || this.state.Saving} onClick={() => this.OnAddRow()} >Add Row</button></div>
            </div>
        )
    }
}



