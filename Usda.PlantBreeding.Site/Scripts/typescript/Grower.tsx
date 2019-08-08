import * as React from 'react'
import * as OrderModel from './OrderModels'

export interface Props extends OrderModel.Grower {

}

export class Summary extends React.Component<Props, {}> {

    constructor(props: Props) {
        super(props);
    }

    renderField(val?: string | number, label?: string, className?: string, link?: string): JSX.Element {
        if (val == undefined) {
            return null;
        }

        let valueElem: JSX.Element | string | number = val;
        if (link != undefined) {
            valueElem = <a href={link}>{val}</a>
        }

        return (
            <div className={className}>
                <span className="text-label">{label}</span>
                <span className="text-val">{valueElem}</span>
            </div>
            );
    }
    
    render() {
        return (
            <div className="summary grower">
                {this.renderField(this.props.FullName, "Name:", "full-name")}
                {this.renderField(this.props.MailingName, "Mailing Name:", "mailing-name")}
                {this.renderField(this.props.MobilePhone, "Mobile:", "mobile-num")}
                {this.renderField(this.props.WorkPhone, "Work:", "work-num")}
                {this.renderField(this.props.Email, "Email:", "email", "mailto:" + this.props.Email)}
            </div>
        );
    }
}