import {get} from './ApiStateModels'

export type searchType = "genotype" | "genotypemap" | "location" | "grower"; 


export interface ControlsMap {
    value: string;
    key: number;
    className: string;
}

interface SearchParams {
    kind: searchType;
    params: AutoCompleteSearchParams;
    textKey: number | null;
}

interface BaseParams {
    textVal: string;
}

export interface GenotypeSearchParams extends BaseParams {
    genusId: number;
}

export interface GenotypeSearch extends SearchParams {
    kind: "genotype";
    params: GenotypeSearchParams;
}

export interface GenotypeMapSearchParams extends GenotypeSearchParams {
    mapId: number;
}

export interface GenotypeMapSearch extends SearchParams {
    kind: "genotypemap";
    params: GenotypeMapSearchParams
}

export interface LocationSearchParams extends BaseParams {
    mapFlag: boolean
}

export interface LocationSearch extends SearchParams {
    kind: "location";
    params: LocationSearchParams;
}

export interface GrowerSearch extends SearchParams {
    kind: "grower";
    params: BaseParams;
}

export type AutoCompleteSearch = GenotypeSearch | GenotypeMapSearch | LocationSearch | GrowerSearch;
export type AutoCompleteSearchParams = GenotypeSearchParams | GenotypeMapSearchParams | BaseParams;


export const GenotypeSearchClient = (params: AutoCompleteSearchParams) => get<ControlsMap[]>("/Accessions/Default/SearchAccessions", params) 
export const LocationSearchClient = (params: AutoCompleteSearchParams) => get<ControlsMap[]>("/Admin/Locations/SearchLocations", params)
export const GenotypeMapSearchClient = (params: AutoCompleteSearchParams) => get<ControlsMap[]>("/Maps/PhenotypeEntry/SearchAccessions", params)
export const GrowerSearchClient = (params: AutoCompleteSearchParams) => get<ControlsMap[]>("/Admin/Growers/SearchGrowers", params)
