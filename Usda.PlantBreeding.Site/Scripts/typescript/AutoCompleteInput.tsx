import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as OrderTable from './OrderTable';
import * as Models from './OrderModels';
import * as DropDown from './DropDown';
import * as AutoComplete from './AutoCompleteModels';
import * as ApiState from './ApiStateModels';
import * as $ from 'jquery';

export interface Props {
    searchParams: AutoComplete.AutoCompleteSearch;
    fillText: (params: AutoComplete.AutoCompleteSearch) => void;
    placeHolder?: string | "";
    disabled: boolean;
}

export interface State {
    searchParams: AutoComplete.AutoCompleteSearch;
    itemSearchResult: ApiState.Resource<AutoComplete.ControlsMap[]>;
    modified: boolean;
    className: string;
    dropdownClassName: string;
    dropdownId: string;
}

export class AutoCompleteInput extends React.Component<Props, State> {
    private searched = false;
    private matched = false;
    private focused = false;
    current: number;
    previous: number;

    private timeOutSearch = 250;

    constructor(props: Props) {
        super(props);

        this.state = {
            searchParams: this.props.searchParams,
            itemSearchResult: { kind: "loading" },
            modified: false,
            className: "form-control",
            dropdownClassName: "autocomplete-options",
            dropdownId: ""
        }
        this.current = this.previous = null;
    }

    componentWillReceiveProps(nextProps: Props) {
        if (nextProps.searchParams.textKey !== this.props.searchParams.textKey) {
            this.setState({
                searchParams: nextProps.searchParams
            })
        }
    }

    async callSearch() {
        const itemSearchResult = this.state.itemSearchResult;
        if (itemSearchResult.kind == "success") {
            this.setState({
                itemSearchResult: { kind: "reloading", content: itemSearchResult.content }

            });
        }

        const searchParams = this.state.searchParams;
        switch (searchParams.kind) {
            case "genotype":
                return AutoComplete.GenotypeSearchClient(searchParams.params)
                    .then((data) => this.onSearchSuccess(data))
                    .catch((err) => this.onSearchError(err));
            case "location":
                return AutoComplete.LocationSearchClient(searchParams.params)
                    .then((data) => this.onSearchSuccess(data))
                    .catch((err) => this.onSearchError(err));
            case "genotypemap":
                return AutoComplete.GenotypeMapSearchClient(searchParams.params)
                    .then((data) => this.onSearchSuccess(data))
                    .catch((err) => this.onSearchError(err));
            case "grower":
                return AutoComplete.GrowerSearchClient(searchParams.params)
                    .then((data) => this.onSearchSuccess(data))
                    .catch((err) => this.onSearchError(err));
        }
    }


    onSearchSuccess(result: AutoComplete.ControlsMap[]) {
        const searchParams = this.state.searchParams;
        const items = result.filter(i => i.value.toLowerCase().startsWith(searchParams.params.textVal.toLowerCase()));
        this.setState({
            itemSearchResult: { kind: "success", content: items },
        });
    }

    searchForMatches() {
        const searchParams = this.state.searchParams;
        const result = this.state.itemSearchResult;
        if (result.kind === "success" || result.kind == "reloading") {
            const items = result.content.filter(i => i.value.toLowerCase().startsWith(searchParams.params.textVal.toLowerCase()));
            if (items.length === 1) {
                this.matched = true;
                searchParams.params.textVal = items[0].value;
                searchParams.textKey = items[0].key;
                this.setState({
                    searchParams: searchParams,
                    className: "form-control"
                });
            }
        }
    }

    onSearchError(err: any) {
        console.log(err);
    }

    onChange = (event: React.FormEvent<HTMLInputElement>) => {
        this.searched = false;

        const searchParams = { ...this.state.searchParams };

        const val = event.currentTarget.value;
        searchParams.params.textVal = val;
        let result = this.state.itemSearchResult;
        if (result.kind == "success") {
            result = { kind: "reloading", content: result.content };
        }
        this.setState({
            modified: true,
            searchParams: searchParams,
            itemSearchResult: result
        });
        setTimeout(() => {
            if (!this.searched) {
                this.searched = true;
                this.callSearch();
            }
        }, this.timeOutSearch);
    }

    async contentLoaded() {
        return new Promise((resolve) => {
            if (this.state.itemSearchResult.kind === "success") {
                resolve(true);
            }
        });
    }

    onBlur() {
        this.current = this.previous = null;
        this.focused = false;
        if (this.state.modified) {
            this.searchForMatches();
            if (!this.matched) {
                this.contentLoaded().then(() => {
                    this.searchForMatches();
                    if (!this.matched) {
                        this.onError();
                    }
                    this.props.fillText(this.state.searchParams);
                })

            } else {
                this.props.fillText(this.state.searchParams);
                this.resetForm();
            }
        }

    }

    resetForm() {
        this.matched = false;
        this.setState({
            itemSearchResult: { kind: "loading" },
            className: "form-control",
            modified: false
        });
    }

    onSelect = (event: React.FormEvent<HTMLDivElement>) => {
        const val = event.currentTarget.textContent.trim();
        const searchParams = this.state.searchParams;
        searchParams.params.textVal = val;
        this.setState({
            searchParams: searchParams
        });
    }

    onArrowUp() {
        const result = this.state.itemSearchResult;
        if (result.kind == "success" || result.kind == "reloading") {
            if (this.current == null || this.current == 0 || this.current > (result.content.length - 1)) {
                this.previous = this.current;
                this.current = (result.content.length - 1);
            } else {
                this.previous = this.current;
                this.current -= 1;
            }
        }
    }

    onArrowDown() {
        const result = this.state.itemSearchResult;
        if (result.kind == "success" || result.kind == "reloading") {
            if (this.current == null || this.current == (result.content.length - 1) || (this.current > (result.content.length - 1))) {
                this.previous = this.current;
                this.current = 0;
            } else {
                this.previous = this.current;
                this.current += 1;
            }
        }
    }

    onEnter() {
        const result = this.state.itemSearchResult;
        if (result.kind == "success" || result.kind == "reloading") {
            const val = result.content;
            val[this.current] = result.content[this.current];
            this.setState({
                itemSearchResult: { kind: "success", content: val }
            });
            this.onBlur();
        }
    }

    updateValue() {
        const result = this.state.itemSearchResult;
        if (result.kind == "success" || result.kind == "reloading") {
            const val = result.content;
            val[this.current] = result.content[this.current];
            val[this.current].className = "focused-option";
            if (this.previous != null) {
                val[this.previous].className = "";
            }
            const searchParams = this.state.searchParams;
            searchParams.params.textVal = val[this.current].value;
            this.setState({
                searchParams: searchParams,
                itemSearchResult: { kind: "success", content: val }
            });
        }
    }

    onKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        const result = this.state.itemSearchResult;
        if (result.kind == "success" || result.kind == "reloading") {
            if (event.keyCode == 8) {
                this.current = null;
            }
            if (event.keyCode == 13) {
                this.updateValue();
                this.onEnter();
            } else {
                if (event.keyCode == 38) {
                    this.onArrowUp();
                    this.updateValue();
                } else if (event.keyCode == 40) {
                    this.onArrowDown();
                    this.updateValue();
                }
            }
        }
    }

    onError() {
        this.setState({
            className: "error form-control"
        });
    }

    onFocus = (event: React.FormEvent<HTMLInputElement>) => {
        this.focused = true;
        const elem = $(event.currentTarget);
        if (elem.offset().top > ($(window).scrollTop() + $(window).height() - 300)) {
            this.setState({
                dropdownClassName: "autocomplete-options flipped"
            });
        }
    }


    renderOptions() {
        const result = this.state.itemSearchResult;
        var options = null;
        if (result.kind == "success" || result.kind == "reloading") {
            options = result.content;
        }
        if (options != null && this.focused) {
            return (
                <div className={this.state.dropdownClassName}>
                    {options.map(option => <div className={option.className} key={option.value} onMouseDown={this.onSelect}> {option.value} </div>)}
                </div>
            );
        } else {
            return null;
        }
    }

    render() {
        return (
            <div className="autocomplete">
                <input disabled={this.props.disabled} className={this.state.className} type="text" placeholder={this.props.placeHolder} value={this.state.searchParams.params.textVal}
                    onBlur={() => this.onBlur()}
                    onChange={this.onChange}
                    onFocus={this.onFocus}
                    onKeyDown={this.onKeyDown} />
                {this.renderOptions()}
            </div>
        );
    }
}