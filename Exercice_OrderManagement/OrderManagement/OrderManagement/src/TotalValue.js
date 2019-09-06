import React from 'react';

class TotalValue extends React.Component {
    constructor(props) {
        super(props);
    }


    render() {
        return (
            <div className="col">
                <div className="card border-danger mb-3 m-2" style={{ width: '18rem' }}>
                    <div className="card-header">Total Value :</div>
                    <div className="card-body text-danger">
                        <h5 className="card-title">{this.props.Total} $</h5>
                    </div>
                </div>
            </div>
            );
    }

}

export default TotalValue;
