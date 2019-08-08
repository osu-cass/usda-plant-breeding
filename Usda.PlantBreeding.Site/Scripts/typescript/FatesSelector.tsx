import * as React from "react";
import * as Models from './Models';

export interface FatesSelector {
    expanded: boolean | undefined;
    isBottomHalf: boolean;
    fates: Models.FateVM[];
    activeFates: Models.FateVM[];
    onSelected: (e: React.FormEvent<HTMLDivElement>) => void;
    onBlur: () => void;
    selectedCell?: Models.CellState;
    toggleFate: (fate: Models.FateVM) => void;

}

interface State {}

export class FSComponent extends React.Component<FatesSelector, State> {


    renderFate(fate: Models.FateVM): JSX.Element {
        const fateActive = this.props.activeFates.some(af => af.Id === fate.Id);

        return (
            <div key={fate.Id}
                className={fateActive ? "active" : ""}
                onClick={() => this.props.toggleFate(fate)}>

                {fate.Name}
            </div>
        );
    }


    renderFateOptions(): JSX.Element | null {
        if (!this.props.expanded) {
            return null;
        }

        const fates = this.props.fates;
        const options = fates.map(f => this.renderFate(f));
        const className = this.props.isBottomHalf
            ? "fate-options flipped"
            : "fate-options";
        return (
            <div className={className}>
                {options}
            </div>
        );
    }

    renderFatesSummary(): JSX.Element {
        const fates = this.props.fates || [];
        const countActive = this.props.activeFates.length;

        let fateSummary: string;
        if (countActive === 0) {
            fateSummary = "None";
        } else if (countActive < 3) {
            fateSummary = this.props.activeFates.map(f => f.Label).join("; ");
        } else {
            fateSummary = countActive + " selected";
        }

        const className = "btn btn-default fates-summary" +
            (this.props.expanded
                ? " active"
                : "");

        return (
            <a className={className}>{fateSummary}</a>
        );
    }

    render() {
        const summary = this.renderFatesSummary();
        const optionsContainer = this.renderFateOptions();

        return (
            <td className="fates">
                <div tabIndex={0}
                    onFocus={this.props.onSelected}
                    onBlur={this.props.onBlur}>

                    {summary}
                    {optionsContainer}
                </div>
            </td>
        );
    }
}
