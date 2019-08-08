export type CellState = AnswerCell | CommentCell | FateCell | SortCellState;
export type SortCellState = RowNumCell | PlantNumCell | CreatedDateCell;
export type EntryType = "seedling" | "observation" | "summary";
export type CellType = "answer" | "comment" | "fate" | "row" | "plant-num" | "created-date";;

export interface RowState {
    mcrId: number;
    modified: boolean;
}

export interface PhenotypeEntryVM {
    Fates: FateVM[] | null;
    Map: MapVM | null;
    QuestionOrder: string[];
    QuestionToQuestion: QuestionToQuestion;
    Type: EntryType;
    YearId: number;
    YearName: string;
    GenusId: number;
    Genotype: GenotypeVM | null;
}

export interface PhenotypeRowsVM {
    MapComponentRows: MapComponentSummaryVM[];
    RowIdList: string[];
}

export interface MapComponentSummaryVM {
    Id: number;
    Row: number;
    PickingNumber: string;
    Rep: number;    
    Comments: string | null;
    PlantNum: number;
    Genotype: GenotypeVM;
    QuestionToAnswer: QuestionToAnswer;
    Fates: FateVM[];
    PreviousFates: string | null;
    MapName: string;
    EvalYear: string;

}

export interface QuestionToAnswer {
    [questionId: string]: Answer
}

export interface QuestionToQuestion {
    [questionId: string]: QuestionVM
}

export interface MapVM {
    Id: number;
    Name: string;
    PlantingYear: string;
    PicklistPrefix: string;
    LocationName: string;
    LocationSuffix: string;
    CrossTypeName: string;
    DefaultPlant: string;
    Note: string;
    StartCorner: string;
    EvaluationYear: string;
}

export interface Answer {
    Id: number;
    Text: string | null;
    QuestionId: number;
    GenotypeId: number;
    MapComponentYearsId: number;

}

export interface QuestionVM {
    Id: number;
    Text: string;
    Label: string;
    InputType: string;
}

export interface GenotypeVM {
    Id: number;
    Name: string;
    FemaleParent: string | null;
    MaleParent: string | null;
    CrossTypeName: string | null;
    CreatedDate: string | null;
    CrossName?: string;
    FamilyName?: string;
    Selection?: number;

}

export interface FateVM {
    Id: number;
    Name: string;
    Label: string;
}

interface CellBase {
    type: CellType;
    mcrID: number;
}

//interface SortColumnCellBase {
//    type: CellType;
//    mcrID: (mcs: MapComponentSummaryVM) => number;
//    value: (mcs: MapComponentSummaryVM) => string | number;
//}

export interface AnswerCell extends CellBase {
    type: "answer";
    questionID: string;
    value: string;
}

export interface CommentCell extends CellBase{
    type: "comment";
    value: string;
}

export interface FateCell extends CellBase {
    type: "fate";
    activeFates: FateVM[];
    isBottomHalf: boolean;
}

export interface RowNumCell extends CellBase {
    type: "row";
    value: number;

}

export interface PlantNumCell extends CellBase {
    type: "plant-num";
    value: number;
}

export interface CreatedDateCell extends CellBase {
    type: "created-date";
    value: string;
}


export function isSortColumnState(cellState: CellState): cellState is SortCellState {
    return (<SortCellState>cellState).value !== undefined;
}