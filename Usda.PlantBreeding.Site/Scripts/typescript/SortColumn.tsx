import * as React from "react";
import * as Models from './Models';


export type Header = "Accession" | "Row" | "Plot" | "Rep" | "Picking" | "P1" | "P2" | "Map Name" | "Year" | "Eval. Year" | "Selected";

export enum SortDirection {
    Ascending = 1,
    Descending = -1
}

export interface HeaderSort {
    col: SortColumn;
    direction: SortDirection;
}

export interface SortColumn {
    header: Header;
    className: string;
    accessor: (mcs: Models.MapComponentSummaryVM) => string | number;
    compare: (a: Models.MapComponentSummaryVM, b: Models.MapComponentSummaryVM) => number;
    cellType?: Models.CellType
    inputType?: string | "text";
    inputPattern?: string;
    inputRequired?: boolean;
}

function compareDates(date1: string | "", date2: string | ""): number {
    return (Date.parse(date1) || 0) - (Date.parse(date2) || 0)

}

interface GenotypeComparable {
    FamilyName?: string,
    Selection?: number
}

function compareGenotypeName(a: GenotypeComparable, b: GenotypeComparable): number {
    const familyCompareVal = (a.FamilyName || "").localeCompare(b.FamilyName || "");
    const selectionCompare = (a.Selection || 0) - (b.Selection || 0);
    return familyCompareVal - selectionCompare;
}

export const sortCookiePrefix = {
    Seedling: "phenotypeSeedlingEntry",
    SelectionSummary: "selectionSummary",
    PhenotypeEntry: "phenotypeEntry",
}
export const entryColumns: SortColumn[] = [
    {
        header: "Accession",
        className: "accession",
        accessor: mcs => mcs.Genotype.Name,
        compare: (a, b) => compareGenotypeName(a.Genotype, b.Genotype),
    },
    {
        header: "Row",
        className: "plot-info",
        accessor: mcs => mcs.Row,
        compare: (a, b) => a.Row - b.Row,
    },
    {
        header: "Plot",
        className: "plot-info",
        accessor: mcs => mcs.PlantNum,
        compare: (a, b) => a.PlantNum - b.PlantNum,
    },
    {
        header: "Rep",
        className: "plot-info",
        accessor: mcs => mcs.Rep,
        compare: (a, b) => a.Rep - b.Rep,
    },
    {
        header: "Picking",
        className: "picking-number",
        accessor: mcs => mcs.PickingNumber,
        compare: (a, b) => a.PickingNumber.localeCompare(b.PickingNumber),
    },
    {
        header: "P1",
        className: "parents",
        accessor: mcs => mcs.Genotype.FemaleParent || "NA",
        compare: (a, b) => (a.Genotype.FemaleParent || "").localeCompare(b.Genotype.FemaleParent || ""),
    },
    {
        header: "P2",
        className: "parents",
        accessor: mcs => mcs.Genotype.MaleParent || "NA",
        compare: (a, b) => (a.Genotype.MaleParent || "").localeCompare(b.Genotype.MaleParent || ""),
    },
];

export const seedlingEntryColumns: SortColumn[] = [
    {
        header: "Accession",
        className: "accession",
        accessor: mcs => mcs.Genotype.Name,
        compare: (a, b) => compareGenotypeName(a.Genotype, b.Genotype),
    },
    {
        header: "Row",
        className: "plot-info",
        accessor: mcs => mcs.Row,
        compare: (a, b) => a.Row - b.Row,
        cellType: "row"
    },
    {
        header: "Plot",
        className: "plot-info",
        accessor: mcs => mcs.PlantNum,
        compare: (a, b) => a.PlantNum - b.PlantNum,
        cellType: "plant-num"
    },
    {
        header: "Picking",
        className: "picking-number",
        accessor: mcs => mcs.PickingNumber,
        compare: (a, b) => a.PickingNumber.localeCompare(b.PickingNumber),
    },
    {
        header: "P1",
        className: "parents",
        accessor: mcs => mcs.Genotype.FemaleParent || "NA",
        compare: (a, b) => (a.Genotype.FemaleParent || "").localeCompare(b.Genotype.FemaleParent || ""),
    },
    {
        header: "P2",
        className: "parents",
        accessor: mcs => mcs.Genotype.MaleParent || "NA",
        compare: (a, b) => (a.Genotype.MaleParent || "").localeCompare(b.Genotype.MaleParent || ""),
    },
    {
        header: "Selected",
        className: "created-date",
        accessor: mcs => mcs.Genotype.CreatedDate || "",
        compare: (a, b) => compareDates(a.Genotype.CreatedDate, b.Genotype.CreatedDate),
        cellType: "created-date",
        inputType: "date",
        inputPattern: "[0-9]{4}-[0-9]{2}-[0-9]{2}",
        inputRequired: true
    },
];



export const summaryColumns: SortColumn[] = [
    {
        header: "Map Name",
        className: "map-name",
        accessor: mcs => mcs.MapName,
        compare: (a, b) => a.MapName.localeCompare(b.MapName),
    },

    {
        header: "Picking",
        className: "picking-number",
        accessor: mcs => mcs.PickingNumber,
        compare: (a, b) => a.PickingNumber.localeCompare(b.PickingNumber),
    },
    {
        header: "Rep",
        className: "plot-info",
        accessor: mcs => mcs.Rep,
        compare: (a, b) => a.Rep - b.Rep,
    },
    {
        header: "Eval. Year",
        className: "map-eval-year",
        accessor: mcs => mcs.EvalYear,
        compare: (a, b) => +a.EvalYear - +b.EvalYear,
    }

];

interface HeaderProps {
    columns: SortColumn[];
    onHeaderClick: (header: SortColumn) => void;
    sort: HeaderSort[];
}


export class TableHeaders extends React.Component<HeaderProps, {}> {
    renderHeader(col: SortColumn): JSX.Element {
        let dirElem: string | undefined;
        const headerSort = this.props.sort.find(hs => hs.col.header === col.header);
        if (!headerSort) {
            dirElem = undefined;
        } else if (headerSort.direction === SortDirection.Ascending) {
            dirElem = "▲"; // TODO: aria
        } else {
            dirElem = "▼";
        }

        return (
            <td key={col.header}
                className={col.className}
                onClick={() => this.props.onHeaderClick(col)}>
                <div className={col.className}>
                    {dirElem} {col.header}
                </div>
            </td>
        );
    }

    render() {
        return (
            <table className="table phenotype-table mapcomponent-table">
                <thead>
                    <tr className="primary">
                        {this.props.columns.map(col => this.renderHeader(col))}
                    </tr>
                </thead>
            </table>
        );
    }
}