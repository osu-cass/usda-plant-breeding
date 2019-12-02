import * as React from "react";
import * as ReactDOM from "react-dom";
import * as $ from "jquery";
import * as SortColumn from './SortColumn'
import * as Models from './Models'
import * as MapSummaryHeader from './MapSummaryHeader'
import * as QuestionRow from './QuestionRow'
import * as PhenotypeAddRow from './PhenotypeAddRow'
import { post } from "./ApiStateModels";
import * as MapComponentRow from "./MapComponentRow"
import * as ApiState from "./ApiStateModels"
import { entryColumns } from "./SortColumn";
import { seedlingEntryColumns } from "./SortColumn";
import { summaryColumns } from "./SortColumn";
import { sortCookiePrefix } from "./SortColumn";


namespace PhenotypeEntry {

    export interface PhenotypeRowsParams {
        MapId?: number;
        Year?: string;
        GenotypeId?: number
    }

    export interface Props {
        columns: SortColumn.SortColumn[];
        paginate: boolean;
        allowAddRow: boolean;
        phenotypeEntryVM: Models.PhenotypeEntryVM;
    }

    export interface State {
        selectedRow?: number; // Show only MCRs with this row number. This doesn't refer to a single MCR.
        sorts?: SortColumn.HeaderSort[];
        phenotypeRows: ApiState.Resource<Models.PhenotypeRowsVM>;
    }

    export class Container extends React.Component<Props, State> {
        refScrollingHeaders: HTMLDivElement;
        refBodies: HTMLDivElement;
        refMCBody: HTMLTableElement;

        constructor(props: Props) {
            super(props);
            this.state = {
                selectedRow: 1, sorts: [],
                phenotypeRows: {
                    kind: "loading"
                }
            };
            this.loadPhenotypeEntry();
        }

        async loadPhenotypeEntry() {
            const vm = this.props.phenotypeEntryVM;
            const params: PhenotypeRowsParams = {
                MapId: (vm.Map) ? vm.Map.Id : undefined,
                GenotypeId: (vm.Genotype) ? vm.Genotype.Id : undefined,
                Year: vm.YearName
            };

            GetPhenotypeVmClient(params)
                .then((data) => this.onPhenotypeRowsSuccess(data))
                .catch(() => null);
        }

        onPhenotypeRowsSuccess(data: Models.PhenotypeRowsVM) {
            this.setState({
                phenotypeRows: { kind: "success", content: data }
            });
        }

        componentDidMount() {
            this.setCookieState();
            this.refBodies.addEventListener("scroll", e => {
                this.refScrollingHeaders.scrollLeft = this.refBodies.scrollLeft;
                this.refMCBody.style.left = this.refBodies.scrollLeft + "px";
            });
        }

        onRowSelected(newRow: number) {
            this.setState({
                selectedRow: newRow
            });
        }

        onMCHeaderClick = (col: SortColumn.SortColumn) => {
            const newSorts = (this.state.sorts || []).slice();
            const headIdx = newSorts.findIndex(hs => hs.col.header === col.header);
            if (headIdx !== -1) {
                const newSort = Object.assign({}, newSorts[headIdx]);
                newSort.direction = newSort.direction === SortColumn.SortDirection.Ascending
                    ? SortColumn.SortDirection.Descending
                    : SortColumn.SortDirection.Ascending;
                newSorts[headIdx] = newSort;
            } else {
                const newSort: SortColumn.HeaderSort = {
                    col: col,
                    direction: SortColumn.SortDirection.Ascending
                };
                newSorts.push(newSort);
            }

            this.setSortCookie(newSorts);
            this.setState({ sorts: newSorts });
        }

        clearSort = () => {
            this.setState({ sorts: [] });
            this.clearSortCookie();
        }

        compareMCRs(lhs: Models.MapComponentSummaryVM, rhs: Models.MapComponentSummaryVM): number {
            const sorts = this.state.sorts || [];
            for (const sort of sorts) {
                const diff = sort.col.compare(lhs, rhs) * sort.direction;
                if (diff !== 0) {
                    return diff;
                }
            }

            return 0;
        }

        getDisplayMCRs(): Models.MapComponentSummaryVM[] {
            let rows: Models.MapComponentSummaryVM[] = [];

            if (this.state.phenotypeRows.kind == "success") {
                let mcrs = this.state.phenotypeRows.content.MapComponentRows;
                if (this.props.paginate) {
                    mcrs = mcrs.filter(mcr => mcr.Row === this.state.selectedRow);
                }
                const sorts = this.state.sorts || [];
                rows = sorts && sorts.length !== 0
                    ? mcrs.stableSort((lhs, rhs) => this.compareMCRs(lhs, rhs))
                    : mcrs;
            }
            return rows;
        }

        renderQuestionHeader(question: Models.QuestionVM): JSX.Element {
            const label = question.Label;
            return (
                <td key={question.Id} id={label}>
                    <div>
                        <label htmlFor={label} title={question.Text}>{label}</label>
                    </div>
                </td>
            );
        }

        renderHeader(): JSX.Element {
            const questionDict = this.props.phenotypeEntryVM.QuestionToQuestion;
            const headers = this.props.phenotypeEntryVM.QuestionOrder.map(qID => this.renderQuestionHeader(questionDict[qID]));

            return <tr className="primary">
                {headers}

                <td className="fates">
                    <div>Fates</div>
                </td>
                <td className="comments">
                    <div>MC Comments</div>
                </td>
                <td className="fates previous-fates">
                    <div>Prev.Fates</div>
                </td>
                <td className="spacer-cell"></td>
            </tr>;
        }

        renderPageList() {
            if (!this.props.paginate || this.state.phenotypeRows.kind != "success") {
                return null;
            }

            let highestRow = 1;
            for (const mcr of this.state.phenotypeRows.content.MapComponentRows) {
                highestRow = Math.max(highestRow, mcr.Row);
            }

            const pages: JSX.Element[] = [];
            for (let i = 1; i <= highestRow; i++) {
                const className = i === this.state.selectedRow
                    ? "active"
                    : "";

                pages[i] = <li key={i} className={className} onClick={() => this.onRowSelected(i)}><a>{i}</a></li>;
            }

            return (
                <div>
                    <ul className="pagination">
                        {pages}
                    </ul>
                </div>
            );
        }

        renderAddRow(): JSX.Element {
            if (this.props.allowAddRow) {
                const pe = this.props.phenotypeEntryVM;
                const mapInfo: PhenotypeAddRow.SeedlingMapInfo = {
                    MapId: pe.Map.Id,
                    GenusId: pe.GenusId,
                    MapYearId: pe.YearId
                };

                return <PhenotypeAddRow.SeedlingRow MapInfo={mapInfo} OnAddRow={(row) => this.onAddSeedlingRow(row)} />

            } else {
                return null;
            }

        }

        isLoading() {
            return this.state.phenotypeRows.kind === "loading" || this.state.phenotypeRows.kind === "reloading";
        }

        render() {
            const displayMCRs = this.getDisplayMCRs();
            const sort = this.state.sorts || []
            const map = (this.props.phenotypeEntryVM.Map) ? <MapSummaryHeader.MapSummaryHeaderComponent{...this.props.phenotypeEntryVM.Map} /> : null;
            return (
                <div className="phenotype-root">
                    {map}
                    {this.renderAddRow()}
                    <div className="phenotype-controls">
                        <button className="clear-sort" onClick={this.clearSort}>Clear Sort</button>
                        {this.renderPageList()}
                    </div>
                    <div className="phenotype-tables">
                        {this.isLoading() ? <img src="/content/images/spin-large.gif" className="spin" /> : undefined}

                        <div className="headers">
                            <SortColumn.TableHeaders
                                sort={sort}
                                onHeaderClick={this.onMCHeaderClick}
                                columns={this.props.columns} />
                            <div className="scrolling-headers" ref={ref => this.refScrollingHeaders = ref}>
                                <table className="table phenotype-table question-table">
                                    <thead>{this.renderHeader()}</thead>
                                </table>
                            </div>
                        </div>

                        <div className="bodies" ref={ref => this.refBodies = ref}>
                            <MapComponentRow.RowsContainer
                                selectedRow={this.state.selectedRow}
                                mapComponentRows={displayMCRs}
                                sort={sort}
                                tableRef={ref => this.refMCBody = ref}
                                columns={this.props.columns}
                                onPlantNumUpdate={this.onPlantNumUpdate}
                                onCreatedDateUpdate={this.onCreatedDateUpdate}
                                onRowNumberUpdate={this.onRowNumberUpdate}
                            />
                            <div className="spacer"></div>
                            <QuestionRow.RowsContainer
                                refBodies={this.refBodies}
                                mapComponentRows={displayMCRs}
                                questionOrder={this.props.phenotypeEntryVM.QuestionOrder}
                                fates={this.props.phenotypeEntryVM.Fates || []}
                                questions={this.props.phenotypeEntryVM.QuestionToQuestion}
                                onAnswerBlur={this.onAnswerBlur}
                                onCommentBlur={this.onCommentBlur}
                                onFatesBlur={this.onFatesBlur}

                            />
                        </div>
                    </div>
                </div>
            );
        }

        onAnswerBlur = (mcrID: number, questionID: string, answerText: string) => {
            this.updateMapComponentRow(mcrID, undefined, undefined, undefined, undefined, { questionID: questionID, answerText: answerText });
        }

        onErrorAnswer(mcsRow: Models.MapComponentSummaryVM) {
            console.log("TODO: add error");
        }

        onFatesBlur = (mcrID: number, activeFates: Models.FateVM[]) => {
            this.updateMapComponentRow(mcrID, undefined, undefined, undefined, undefined, null, activeFates);

        }

        onCommentBlur = (mcrID: number, newComment: string) => {
            this.updateMapComponentRow(mcrID, undefined, undefined, undefined, newComment);
        }

        onPlantNumUpdate = (mcrID: number, val: number) => {
            this.updateMapComponentRow(mcrID, val, undefined, undefined, undefined);
        }

        onRowNumberUpdate = (mcrID: number, val: number) => {
            this.updateMapComponentRow(mcrID, undefined, val, undefined, undefined);
        }

        onCreatedDateUpdate = (mcrID: number, val: string) => {
            this.updateMapComponentRow(mcrID, undefined, undefined, val, undefined);
        }

        //calls api to update passed in params
        updateMapComponentRow(mcrID: number, plantNum?: number,
            rowNum?: number, createdDate?: string, comment?: string,
            question: { questionID: string, answerText: string } = null,
            activeFates: Models.FateVM[] = null) {

            if (this.state.phenotypeRows.kind == "success") {
                const newModel = Object.assign({}, this.state.phenotypeRows.content);
                newModel.MapComponentRows = newModel.MapComponentRows.slice();
                const row = newModel.MapComponentRows.findIndex(mcr => mcr.Id === mcrID);
                if (row === -1) {
                    console.error("Failed to find row with ID " + mcrID + ".");
                    return;
                }

                const mcr = newModel.MapComponentRows[row];
                //Plant Num 
                if (plantNum != undefined) {
                    mcr.PlantNum = plantNum;
                    SavePlantNumberClient(mcr)
                        .then(data => null)
                        .catch(err => null);
                }

                //Row Num 
                if (rowNum != undefined) {
                    mcr.Row = rowNum;
                    SaveRowNumberClient(mcr)
                        .then(data => null)
                        .catch(err => null);
                }

                //Created Date
                if (createdDate != undefined) {
                    mcr.Genotype.CreatedDate = createdDate;
                    SaveCreatedDateClient(mcr)
                        .then(data => null)
                        .catch(err => null);
                }

                //Comment
                if (comment != undefined) {
                    mcr.Comments = comment;
                    SaveCommentsClient(mcr)
                        .then(data => null)
                        .catch(err => null);
                }

                //Question
                if (question != null) {
                    mcr.QuestionToAnswer = Object.assign({}, mcr.QuestionToAnswer);
                    mcr.QuestionToAnswer[question.questionID].Text = question.answerText;
                    SaveAnswerClient(mcr.QuestionToAnswer[question.questionID])
                        .then(data => null)
                        .catch(err => this.onErrorAnswer(mcr));
                }

                //Active fates
                if (activeFates != null) {
                    mcr.Fates = activeFates.map(f => ({ Id: f.Id, Name: f.Name, Label: f.Label }));
                    SaveFatesClient({ fates: activeFates.map(f => f.Id), mcrid: mcrID })
                        .then(data => null)
                        .catch(err => null);
                }

                this.setState({
                    phenotypeRows: { kind: "success", content: newModel }
                });
            }

        }

        onErrorComments(mcsRow: Models.MapComponentSummaryVM) {
            console.log("Todo: add error");
        }

        onAddSeedlingRow = (row: Models.MapComponentSummaryVM) => {
            if (this.state.phenotypeRows.kind == "success") {
                const newModel = Object.assign({}, this.state.phenotypeRows.content);
                newModel.MapComponentRows = newModel.MapComponentRows.slice();
                newModel.MapComponentRows.unshift(row);
                this.setState({
                    phenotypeRows: { kind: "success", content: newModel },
                    sorts: []
                });
            }
        }

        //Set the sorting cookie for the current page. Cookies are set to last for 10 years. Cookies follow the format [pagePrefix]SortCookie.
        setSortCookie(newSort: SortColumn.HeaderSort[]) {
            var data = JSON.stringify(newSort);
            var date = new Date();
            var combstr = "";
            const millisecondYear = 365 * 24 * 60 * 60 * 1000;
            date.setTime(date.getTime() + (10 * millisecondYear));
            //Check for the created-date column in the phenotype entry table, which is only present on seedling maps
            if (document.getElementById("phenotype-entry").getElementsByClassName("created-date").length !== 0) {
                combstr = sortCookiePrefix.Seedling + "SortCookie=" + data + "; expires=" + date.toUTCString();
            }
            //Check for the map-name column, which is only present on the selection summary
            else if (document.getElementById("phenotype-entry").getElementsByClassName("map-name").length !== 0) {
                combstr = sortCookiePrefix.SelectionSummary + "SortCookie=" + data + "; expires=" + date.toUTCString();
            }
            //Default case, which is applied on non-seedling map phenotype entries
            else {
                combstr = sortCookiePrefix.PhenotypeEntry + "SortCookie=" + data + "; expires=" + date.toUTCString();
            }
            document.cookie = combstr;
        }

        // Returns the HeaderSort array stored in a cookie for the current page, or null if the cookie does not exist. 
        getSortCookie(): SortColumn.HeaderSort[] {
            var data = document.cookie;
            var split = data.split(';');
            for (var i = 0; i < split.length; i++) {
                var s = split[i];
                //Check for the created-date column in the phenotype entry table, which is only present on seedling maps
                if (document.getElementById("phenotype-entry").getElementsByClassName("created-date").length !== 0) {
                    if (s.includes(sortCookiePrefix.Seedling + "SortCookie")) {
                        //The cookie has the format phenotypeSeedlingEntrySortCookie=[JSONdata]
                        var subsplit = s.split('=');
                        let result: SortColumn.HeaderSort[] = [];
                        var obj = JSON.parse(subsplit[1]);

                        //Parse object to specific SortColumns
                        for (var i = 0; i < obj.length; i++) {
                            let col: SortColumn.SortColumn = obj[i].col;
                            //We cannot store the necessary classes in a cookie (as it is a string), so instead convert based on stored header value
                            switch (obj[i].col.header) {
                                case "Accession":
                                    col = seedlingEntryColumns.find(({ header }) => header === "Accession");
                                    break;
                                case "Row":
                                    col = seedlingEntryColumns.find(({ header }) => header === "Row");
                                    break;
                                case "Plot":
                                    col = seedlingEntryColumns.find(({ header }) => header === "Plot");
                                    break;
                                case "Selected":
                                    col = seedlingEntryColumns.find(({ header }) => header === "Selected");
                                    break;
                                case "Picking":
                                    col = seedlingEntryColumns.find(({ header }) => header === "Picking");
                                    break;
                                case "P1":
                                    col = seedlingEntryColumns.find(({ header }) => header === "P1");
                                    break;
                                case "P2":
                                    col = seedlingEntryColumns.find(({ header }) => header === "P2");
                                    break;
                            }
                            let item: SortColumn.HeaderSort = { col: col, direction: obj[i].direction };
                            result.push(item);
                        }
                        return result;
                    }
                }
                //Check for the map-name column, which is only present on the selection summary
                else if (document.getElementById("phenotype-entry").getElementsByClassName("map-name").length !== 0) {
                    if (s.includes(sortCookiePrefix.SelectionSummary + "SortCookie")) {
                        //The cookie has the format selectionSummarySortCookie=[JSONdata]
                        var subsplit = s.split('=');
                        let result: SortColumn.HeaderSort[] = [];
                        var obj = JSON.parse(subsplit[1]);

                        //Parse object to specific SortColumns
                        for (var i = 0; i < obj.length; i++) {
                            let col: SortColumn.SortColumn = obj[i].col;
                            //We cannot store the necessary classes in a cookie (as it is a string), so instead convert based on stored header value
                            switch (obj[i].col.header) {
                                case "Map Name":
                                    col = summaryColumns.find(({ header }) => header === "Map Name");
                                    break;
                                case "Picking":
                                    col = summaryColumns.find(({ header }) => header === "Picking");
                                    break;
                                case "Rep":
                                    col = summaryColumns.find(({ header }) => header === "Rep");
                                    break;
                                case "Eval. Year":
                                    col = summaryColumns.find(({ header }) => header === "Eval. Year");
                                    break;
                            }
                            let item: SortColumn.HeaderSort = { col: col, direction: obj[i].direction };
                            result.push(item);
                        }
                        return result;
                    }
                }
                //Default case, which is applied on non-seedling map phenotype entries
                else {
                    if (s.includes(sortCookiePrefix.PhenotypeEntry + "SortCookie")) {

                        //The cookie has the format phenotypeEntrySortCookie=[JSONdata]
                        var subsplit = s.split('=');
                        let result: SortColumn.HeaderSort[] = [];
                        var obj = JSON.parse(subsplit[1]);

                        //Parse object to specific SortColumns
                        for (var i = 0; i < obj.length; i++) {
                            let col: SortColumn.SortColumn = obj[i].col;
                            //We cannot store the necessary classes in a cookie (as it is a string), so instead convert based on stored header value
                            switch (obj[i].col.header) {
                                case "Accession":
                                    col = entryColumns.find(({ header }) => header === "Accession");
                                    break;
                                case "Row":
                                    col = entryColumns.find(({ header }) => header === "Row");
                                    break;
                                case "Plot":
                                    col = entryColumns.find(({ header }) => header === "Plot");
                                    break;
                                case "Rep":
                                    col = entryColumns.find(({ header }) => header === "Rep");
                                    break;
                                case "Picking":
                                    col = entryColumns.find(({ header }) => header === "Picking");
                                    break;
                                case "P1":
                                    col = entryColumns.find(({ header }) => header === "P1");
                                    break;
                                case "P2":
                                    col = entryColumns.find(({ header }) => header === "P2");
                                    break;
                            }
                            let item: SortColumn.HeaderSort = { col: col, direction: obj[i].direction };
                            result.push(item);
                        }
                        return result;
                    }
                }
            }
            return null;
        }

        //Set the sort order based on the sort cookie for the page
        setCookieState() {
            var cookieSorts = this.getSortCookie();
            if (cookieSorts != null) {
                this.setState({ sorts: cookieSorts });
            }
        }

        //Replace the sort cookie for the page with an empty array
        clearSortCookie() {
            var data: SortColumn.HeaderSort[] = [];
            this.setSortCookie(data);
        }
    }
}

///Clients
export const SavePlantNumberClient = (params: Models.MapComponentSummaryVM) =>
    post<Models.MapComponentSummaryVM>("/Maps/PhenotypeEntry/SavePlantNum", params)

export const SaveRowNumberClient = (params: Models.MapComponentSummaryVM) =>
    post<Models.MapComponentSummaryVM>("/Maps/PhenotypeEntry/SaveRowNum", params)

export const SaveCreatedDateClient = (params: Models.MapComponentSummaryVM) =>
    post<Models.MapComponentSummaryVM>("/Maps/PhenotypeEntry/SaveDateCreated", params)

export const SaveAnswerClient = (params: Models.Answer) =>
    post<Models.Answer>("/Maps/PhenotypeEntry/SaveAnswerJson?token=" + params.MapComponentYearsId + params.QuestionId, params)

export const SaveCommentsClient = (params: Models.MapComponentSummaryVM) =>
    post<Models.MapComponentSummaryVM>("/Maps/PhenotypeEntry/SaveCommentsJson?token=" + params.Id, params)

export const SaveFatesClient = (params: { fates: number[], mcrid: number }) =>
    post<Models.MapComponentSummaryVM>("/Maps/PhenotypeEntry/SaveFatesJson?token=" + params.mcrid, params)

export const GetPhenotypeVmClient = (params: PhenotypeEntry.PhenotypeRowsParams) =>
    ApiState.get<Models.PhenotypeRowsVM>("/Maps/PhenotypeEntry/GetPhenotypeRows", params)

export function initializeEntryPage(vm: Models.PhenotypeEntryVM, rootDiv: HTMLDivElement) {
    const containerProps: PhenotypeEntry.Props = {
        allowAddRow: false,
        columns: [],
        paginate: false,
        phenotypeEntryVM: vm
    };

    if (vm.Type == "seedling") {
        containerProps.paginate = false;
        containerProps.allowAddRow = true;
        containerProps.columns = SortColumn.seedlingEntryColumns;

    } else if (vm.Type == "observation") {
        containerProps.paginate = true;
        containerProps.allowAddRow = false;
        containerProps.columns = SortColumn.entryColumns;
    }

    ReactDOM.render(<PhenotypeEntry.Container {...containerProps} />, rootDiv);
}

export function initializeSummaryPage(vm: Models.PhenotypeEntryVM, rootDiv: HTMLDivElement) {
    const containerProps: PhenotypeEntry.Props = {
        allowAddRow: false,
        columns: SortColumn.summaryColumns,
        paginate: false,
        phenotypeEntryVM: vm
    };

    ReactDOM.render(<PhenotypeEntry.Container {...containerProps} />, rootDiv);
}