import * as React from "react";
import * as Models from './Models';

export class MapSummaryHeaderComponent extends React.Component<Models.MapVM, {}>{
    render() {
        return (
            <div className="page-info-block">
                <div className="map-summary">

                    <p>
                        <span className="text-label">Picking Prefix:</span>
                        <span className="text-value"> {this.props.PicklistPrefix}</span>
                    </p>
                    <p>
                        <span className="text-label">Established Year:</span>
                        <span className="text-value"> {this.props.PlantingYear}</span>
                    </p>

                    <p>
                        <span className="text-label">Location:</span>
                        <span className="text-value"> {this.props.LocationName}</span>
                    </p>
                    <p>
                        <span className="text-label">Desc. Code:</span>
                        <span className="text-value"> {this.props.LocationSuffix}</span>
                    </p>

                    <p>
                        <span className="text-label">Cross Type:</span>
                        <span className="text-value"> {this.props.CrossTypeName}</span>
                    </p>
                    <p>
                        <span className="text-label">Default # Plts:</span>
                        <span className="text-value"> {this.props.DefaultPlant}</span>
                    </p>

                    <p className="map-note">
                        <span className="text-label">Note:</span>
                        <span className="text-value note"> {this.props.Note}</span>
                    </p>
                    <p>
                        <span className="text-label">Start Corner:</span>
                        <span className="text-value"> {this.props.StartCorner}</span>
                    </p>


                    <p>
                        <span className="text-label important">Eval Year:</span>
                        <span className="text-value important"> {this.props.EvaluationYear}</span>
                    </p>
                </div>
            </div>
        );
    }
}
