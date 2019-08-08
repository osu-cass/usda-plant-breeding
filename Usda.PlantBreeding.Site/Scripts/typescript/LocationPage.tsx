import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as Models from './OrderModels';
import * as Location from './Locations'

namespace LocationPage {
    export class Container extends React.Component<Models.Location, {}> {
        onSuccess = () => {
            location.href = "/Admin/Locations/Index?mapFlag=" + this.props.MapFlag;
        }
        render() {
            return (
                <Location.EditComponent Location={this.props} OnSaveSuccess={this.onSuccess}  />
                );
        }
    }

}

export function initLocationEditPage(pageVM: Models.Location) {
    const rootDiv = document.getElementById("locations-page") as HTMLDivElement;
    ReactDOM.render(<LocationPage.Container {...pageVM} />, rootDiv);
}
