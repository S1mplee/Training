import React from 'react';

class Asset extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="card-body text-primary ">
                <ul>
                    {Object.keys(this.props.dict).map((key, index) =>
                        <li key={index}>{key} Quantite : {this.props.dict[key].Quantite} Price : {this.props.dict[key].price}</li>
                    )}
                </ul>
            </div>
            );
    }








}

export default Asset;

