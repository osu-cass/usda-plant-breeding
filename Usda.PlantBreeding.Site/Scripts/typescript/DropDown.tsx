import * as React from 'react'

export interface Selection {
    label: string;
    selectionCode: string;
    order: number;
    disabled: boolean;
}

export interface Props {
    label: string | null;
    selectedCode: string | null;
    selections: Selection[];
    updateSelection(selectionCode: string): void;
    resourceCode: string;
    inputDisabled: boolean;
}

export interface State {
    label: string | null;
    selectedCode: string | null;
    selections: Selection[];
    resourceCode: string;
}

export class Dropdown extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);

        this.state = {
            selectedCode: this.props.selectedCode,
            label: this.props.label,
            selections: this.props.selections,
            resourceCode: this.props.resourceCode
        }
    }

    onChange = (event: React.FormEvent<HTMLSelectElement>) => {
        const val = event.currentTarget.value;
        this.props.updateSelection(val);
        this.updateState(event);
    }

    updateState = (event: React.FormEvent<HTMLSelectElement>) => {
        const val = event.currentTarget.value;
        this.setState({
            selectedCode: val,
            label: val.toString()
        });
    }

    renderOption = (selection: Selection) => {
        const label = selection.label;
        return (
            <option value={selection.selectionCode}
                aria-label={selection.label}
                key={selection.selectionCode}
                disabled={selection.disabled}>
                {label}
            </option>
        )
    }

    render() {
        const options = this.state.selections.map(this.renderOption);
        return (
            <div>
                <select disabled={this.props.inputDisabled}
                    className="form-control"
                    onChange={this.onChange}
                    id={this.state.resourceCode}
                    value={this.props.selectedCode}>
                    {options}
                </select>
            </div>
        )
    }
}