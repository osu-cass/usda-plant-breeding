import * as React from "react";
import * as Models from './Models'
import * as FatesSelector from './FatesSelector'

export interface Props {
    refBodies: HTMLDivElement;
    mapComponentRows: Models.MapComponentSummaryVM[];
    questionOrder: string[];
    fates: Models.FateVM[];
    questions: Models.QuestionToQuestion;
    onAnswerBlur: (mcrID: number, questionID: string, answerText: string) => void;
    onCommentBlur: (mcrID: number, newComment: string) => void;
    onFatesBlur: (mcrID: number, activeFates: Models.FateVM[]) => void;

}

export interface State {
    selectedCell?: Models.CellState;
}

export class RowsContainer extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {};
    }

    onToggleFate = (fate: Models.FateVM) => {
        const cell = this.state.selectedCell;
        if (!cell) {
            console.error("No cell was selected");
            return;
        }

        if (cell.type !== "fate") {
            console.error("Expected a fate cell, but found:");
            console.error(cell);
            return;
        }

        let newActiveFates: Models.FateVM[];
        const fateIdx = cell.activeFates.findIndex(af => af.Id === fate.Id);
        if (fateIdx === -1) {
            newActiveFates = [fate, ...cell.activeFates];
        } else {
            newActiveFates = cell.activeFates.filter(af => af.Id !== fate.Id);
        }

        const newCell = Object.assign({}, cell, { activeFates: newActiveFates });

        this.setState({ selectedCell: newCell });
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

        if (cell.type === "comment" && cell.value !== mcr.Comments) {
            const didChange = cell.value !== mcr.Comments;
            if (didChange) {
                this.props.onCommentBlur(mcr.Id, cell.value);
            }
        } else if (cell.type === "answer") {
            const didChange = cell.value !== mcr.QuestionToAnswer[cell.questionID].Text;
            if (didChange) {
                this.props.onAnswerBlur(mcr.Id, cell.questionID, cell.value);
            }
        } else if (cell.type === "fate") {
            const masterFates = this.props.fates || [];
            const didChange = masterFates.some(mf => {
                const propsFateActive = mcr.Fates.some(f => f.Id === mf.Id);
                const stateFateActive = cell.activeFates.some(f => f.Id === mf.Id);
                return propsFateActive === stateFateActive;
            });

            if (didChange) {
                this.props.onFatesBlur(mcr.Id, cell.activeFates);
            }
        }

        this.setState({
            selectedCell: undefined
        })

    }


    onAnswerSelected(questionID: string, mcrID: number) {
        const mcr = this.props.mapComponentRows.find(mcr => mcr.Id === mcrID);
        if (!mcr) {
            console.error("Failed to find MCR with ID: " + mcrID);
            return;
        }

        const value = mcr.QuestionToAnswer[questionID].Text || "";
        this.setState({
            selectedCell: {
                type: "answer",
                mcrID: mcr.Id,
                questionID: questionID,
                value: value
            }
        });
    }


    onCommentsSelected(mcrID: number) {
        const mcr = this.props.mapComponentRows.find(mcr => mcr.Id === mcrID);
        if (!mcr) {
            console.error("Failed to find MCR with ID: " + mcrID);
            return;
        }

        const value = mcr.Comments || "";
        this.setState({
            selectedCell: {
                mcrID: mcr.Id,
                type: "comment",
                value: value
            }
        });
    }

    onFatesSelected(e: React.FormEvent<HTMLDivElement>, mcrID: number) {
        const target = (e.target as HTMLElement);
        const targetTop = target.getBoundingClientRect().top;
        const bodiesRect = this.props.refBodies.getBoundingClientRect();
        const containerTop = bodiesRect.top;
        const targetRelativeTop = targetTop - containerTop;
        const isBottomHalf = targetRelativeTop > (bodiesRect.height / 2);

        const mcr = this.props.mapComponentRows.find(mcr => mcr.Id === mcrID);
        if (!mcr) {
            console.error("Failed to find MCR with ID: " + mcrID);
            return;
        }

        const currentCell = this.state.selectedCell;
        if (currentCell && currentCell.mcrID === mcr.Id && currentCell.type === "fate") {
            return; // Fate cell is already selected; don't change state
        }

        this.setState({
            selectedCell: {
                mcrID: mcr.Id,
                type: "fate",
                activeFates: mcr.Fates,
                isBottomHalf: isBottomHalf
            }
        });
    }


    renderBody(): JSX.Element[] {
        const rows = this.props.mapComponentRows.map(mcr =>
            <QRComponent
                key={mcr.Id}
                summary={mcr}
                questionOrder={this.props.questionOrder}
                fates={this.props.fates || []}
                questions={this.props.questions}
                selectedCell={this.state.selectedCell}
                onAnswerSelected={qID => this.onAnswerSelected(qID, mcr.Id)}
                onCellValueChange={newValue => this.onCellValueChange(mcr.Id, newValue)}
                onCommentsSelected={() => this.onCommentsSelected(mcr.Id)}
                onFatesSelected={e => this.onFatesSelected(e, mcr.Id)}
                onCellBlur={this.onCellBlur}
                toggleFate={(fateVM) => this.onToggleFate(fateVM)}
            />);
        return rows;
    }




    render() {
        return (

            <table
                className="table table-striped phenotype-table question-table">
                <tbody>
                    {this.renderBody()}
                </tbody>
            </table>
        )
    }

}

export interface QuestionRow {
    summary: Models.MapComponentSummaryVM;
    questionOrder: string[];
    fates: Models.FateVM[];
    questions: Models.QuestionToQuestion;
    onCellValueChange: (newValue: string) => void;
    onAnswerSelected: (questionID: string) => void;
    onCommentsSelected: () => void;
    onFatesSelected: (e: React.FormEvent<HTMLDivElement>) => void;
    onCellBlur: () => void;
    selectedCell?: Models.CellState;
    toggleFate: (fate: Models.FateVM) => void;
}

interface QuestionRowState {}

export class QRComponent extends React.Component<QuestionRow, QuestionRowState> {
    shouldComponentUpdate(nextProps: QuestionRow, nextState: QuestionRowState, nextContext: any): boolean {
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

    renderAnswerCell(mcr: Models.MapComponentSummaryVM, questionID: string): JSX.Element {
        const selectedCell = this.props.selectedCell;
        if (selectedCell &&
            selectedCell.type === "answer" &&
            selectedCell.questionID === questionID &&
            selectedCell.mcrID === mcr.Id) {
            const question = this.props.questions[questionID];

            return (
                <td key={questionID}>
                    <div>
                        <input className="form-control"
                            tabIndex={0}
                            value={selectedCell.value}
                            maxLength={6}
                            autoFocus={true}
                            onChange={e => this.props.onCellValueChange((e.target as HTMLInputElement).value)}
                            onFocus={e => this.handleInputFocus(e.target as HTMLInputElement)}
                            onBlur={this.props.onCellBlur}
                            placeholder={question.Label}
                            type={question.InputType}
                        />
                    </div>
                </td>
            )
        } else {
            const handler = () => this.props.onAnswerSelected(questionID);
            const text = mcr.QuestionToAnswer[questionID].Text;
            return (
                <td key={questionID}
                    tabIndex={0}
                    onFocus={handler}
                    onClick={handler}>

                    <div>
                        {text}
                    </div>
                </td>
            );
        }
    }

    renderCommentCell(mcr: Models.MapComponentSummaryVM): JSX.Element {
        const selectedCell = this.props.selectedCell;
        if (selectedCell &&
            selectedCell.type === "comment" &&
            selectedCell.mcrID === mcr.Id) {

            return (
                <td className="comments">
                    <div>
                        <textarea className="form-control comments-selected"
                            tabIndex={0}
                            value={selectedCell.value}
                            autoFocus={true}
                            onChange={e => this.props.onCellValueChange((e.target as HTMLTextAreaElement).value)}
                            onBlur={this.props.onCellBlur} />
                    </div>
                </td>
            )
        } else {
            const comments = mcr.Comments;
            const handler = this.props.onCommentsSelected;
            return (
                <td className="comments"
                    tabIndex={0}
                    onFocus={handler}
                    onClick={handler}>

                    <p>{comments}</p>
                </td>
            );
        }
    }

    renderFatesCell(mcr: Models.MapComponentSummaryVM): JSX.Element {
        const selectedCell = this.props.selectedCell;

        let activeFates: Models.FateVM[];
        let isExpanded: boolean;
        let isBottomHalf: boolean;
        if (selectedCell &&
            selectedCell.type === "fate" &&
            selectedCell.mcrID === mcr.Id) {
            isExpanded = true;
            isBottomHalf = selectedCell.isBottomHalf;
            activeFates = selectedCell.activeFates;
        } else {
            isExpanded = false;
            isBottomHalf = false;
            activeFates = mcr.Fates;
        }

        return (
            <FatesSelector.FSComponent
                expanded={isExpanded}
                fates={this.props.fates}
                activeFates={activeFates}
                onBlur={this.props.onCellBlur}
                onSelected={this.props.onFatesSelected}
                isBottomHalf={isBottomHalf}
                toggleFate={this.props.toggleFate}
            />
        );
    }

    renderPreviousFateCell(mcr: Models.MapComponentSummaryVM): JSX.Element {
        return (
            <td className="fates previous-fates"
                tabIndex={0}>

                <p>{mcr.PreviousFates}</p>
            </td>
        );
    }

    render() {
        const selectedCell = this.props.selectedCell;
        const cells = this.props.questionOrder.map(qID => this.renderAnswerCell(this.props.summary, qID));
        const comment = this.renderCommentCell(this.props.summary);
        const fate = this.renderFatesCell(this.props.summary);
        const previousFate = this.renderPreviousFateCell(this.props.summary);
        const className = (selectedCell && this.props.summary.Id == selectedCell.mcrID) ? "row-selected" : null;

        return (
            <tr key={this.props.summary.Id} className={className}>
                {cells}
                {fate}
                {comment}
                {previousFate}
            </tr>
        );
    }
}