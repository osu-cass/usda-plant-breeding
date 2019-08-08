import * as React from "react";
import * as Models from './Models';
import * as SortColumn from './SortColumn'
import { post } from "./ApiStateModels";

interface Props {
    tableRef?: (ref: HTMLTableElement) => void;
    mapComponentRows: Models.MapComponentSummaryVM[];
    sort: SortColumn.HeaderSort[];
    columns: SortColumn.SortColumn[];
    selectedRow?: number;
    onPlantNumUpdate: (mcrID: number, val: number) => void;
    onRowNumberUpdate: (mcrID: number, val: number) => void;
    onCreatedDateUpdate: (mcrID: number, val: string) => void;
}

interface State {
    selectedCell?: Models.CellState;

}

export class RowsContainer extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {};
    }

    onSortColumnSelected(mcrID: number, sortColumn: SortColumn.SortColumn) {
        const mcr = this.props.mapComponentRows.find(mcr => mcr.Id === mcrID);
        if (!mcr) {
            console.error("Failed to find MCR with ID: " + mcrID);
            return;
        }

        if (sortColumn.cellType) {
            if (sortColumn.cellType == "plant-num") {
                this.setState({
                    selectedCell: {
                        mcrID: mcr.Id,
                        type: (sortColumn.cellType),
                        value: sortColumn.accessor(mcr) as number
                    }
                });
            } else if (sortColumn.cellType == "row") {
                this.setState({
                    selectedCell: {
                        mcrID: mcr.Id,
                        type: (sortColumn.cellType),
                        value: sortColumn.accessor(mcr) as number
                    }
                });

            } else if (sortColumn.cellType == "created-date") {
                this.setState({
                    selectedCell: {
                        mcrID: mcr.Id,
                        type: (sortColumn.cellType),
                        value: sortColumn.accessor(mcr) as string
                    }
                });
            }
        }
    }

    onCellValueChange(mcrID: number, newValue: string) {
        const cell = this.state.selectedCell;
        if (!cell) {
            console.error("Expected a selected cell");
            return;
        }

        if (cell.type == "fate") {
            console.error("Wrong type of cell " + cell.type);
            console.error(cell);
            return;
        }

        const newCell = Object.assign({}, cell);
        newCell.value = newValue;
        this.setState({
            selectedCell: newCell
        });
    }

    onCellBlur = () => {
        const cell = this.state.selectedCell;
        if (!cell) {
            console.error("Expected a selected cell");
            return;
        }

        const mcr = this.props.mapComponentRows.find(mcr => mcr.Id === cell.mcrID);
        if (!mcr) {
            console.error("Failed to find MCR with ID: " + cell.mcrID);
            return;
        }

        switch (cell.type) {

            case "plant-num":
                if (cell.value !== mcr.PlantNum) {
                    this.props.onPlantNumUpdate(mcr.Id, cell.value);
                }
                break;
            case "row":
                if (cell.value !== mcr.Row) {
                    this.props.onRowNumberUpdate(mcr.Id, cell.value);
                }
                break;
            case "created-date":
                if (cell.value !== mcr.Genotype.CreatedDate) {
                    this.props.onCreatedDateUpdate(mcr.Id, cell.value);
                }
                break;
        }
        this.setState({
            selectedCell: undefined
        });


    }

    renderRows(): JSX.Element {
        const rows = this.props.mapComponentRows.map(mcr =>
            <MCComponentRow
                key={mcr.Id}
                summary={mcr}
                selectedCell={this.state.selectedCell}
                columns={this.props.columns}
                onCellValueChange={(mcrID, newValue) => this.onCellValueChange(mcrID, newValue)}
                onCellBlur={this.onCellBlur}
                onCellSelected={(mcrID, sortColumn) => this.onSortColumnSelected(mcrID, sortColumn)} />);
        return (
            <tbody>{rows}</tbody>
        );
    }

    render(): JSX.Element {
        return (
            <table className="table table-striped phenotype-table mapcomponent-table"
                ref={this.props.tableRef}>

                {this.renderRows()}
            </table>
        );
    }
}

interface RowProps {
    summary: Models.MapComponentSummaryVM;
    columns: SortColumn.SortColumn[];
    selectedCell?: Models.CellState;
    onCellValueChange: (mcrId: number, newValue: string) => void;
    onCellBlur: () => void;
    onCellSelected: (mcrId: number, sortColumn: SortColumn.SortColumn) => void;

}

export class MCComponentRow extends React.Component<RowProps, {}> {
    constructor(props: RowProps) {
        super(props);
        this.state = {};
    }

    shouldComponentUpdate(nextProps: RowProps, nextState: {}, nextContext: any): boolean {
        const currentCell = this.props.selectedCell;
        if (currentCell && currentCell.mcrID === this.props.summary.Id) {
            return true;
        }

        const nextCell = nextProps.selectedCell;
        if (nextCell && nextCell.mcrID === this.props.summary.Id) {
            return true;
        }

        const should = this.props.summary !== nextProps.summary;
        return should;
    }

    handleInputFocus(target: HTMLInputElement) {
        target.select();
    }

    renderCell(col: SortColumn.SortColumn, mcr: Models.MapComponentSummaryVM): JSX.Element {
        const selectedCell = this.props.selectedCell;
        const val = col.accessor(mcr);
        if (selectedCell &&
            Models.isSortColumnState(selectedCell) &&
            selectedCell.type == col.cellType &&
            selectedCell.mcrID === mcr.Id) {
            return (

                <td key={col.header} className={col.className}>
                    <input className="form-control"
                        tabIndex={0}
                        value={selectedCell.value}
                        maxLength={6}
                        autoFocus={true}
                        onChange={e => this.props.onCellValueChange(mcr.Id, (e.target as HTMLInputElement).value)}
                        onFocus={e => this.handleInputFocus(e.target as HTMLInputElement)}
                        onBlur={this.props.onCellBlur}
                        type={col.inputType}
                        pattern={col.inputPattern}
                        required={col.inputRequired}
                    />
                </td>
            )

        } else if (col.cellType) {
            const handler = () => this.props.onCellSelected(mcr.Id, col);

            return (
                <td key={col.header}
                    className={col.className}
                    tabIndex={0}
                    onFocus={handler}
                    onClick={handler}>
                    <div className={col.className}>
                        {val}
                    </div>
                </td>
            );
        }
        else {
            return (
                <td key={col.header} className={col.className} >
                    <div className={col.className}>
                        {val}
                    </div>
                </td>
            );
        }
    }

    render() {
        const mcr = this.props.summary;
        const selectedCell = this.props.selectedCell;
        const className = (selectedCell && mcr.Id == selectedCell.mcrID) ? "row-selected" : null;

        return (
            <tr className={className}>
                {this.props.columns.map(col => this.renderCell(col, mcr))}
            </tr>
        );
    }
}