import React, { Component } from 'react';
import dotnetify from 'dotnetify';

class Edit extends Component {
    constructor(props) {
        super();
        this.vm = dotnetify.react.connect("EditViewModel", this);
        this.state = {
            ID: "",
            over: "",
            daily: "",
            message:""
        };
        this.styles = {
            color: "green"
        };

        this.dispatchState = state => {
            this.setState(state);
            this.vm.$dispatch(state);
        };

        this.state.ID = props.value;
    }

    componentWillUnmount() {
        this.vm.$destroy();
    }
  

    render() {
        return (<div className='popup'>
            <div className='popup_inner'>
                <button className="btn btn-danger m-2" onClick={this.props.closePopup}>x</button>
<div class="card mx-xl-5">
                    <label style={{ color: 'red' }}>{this.state.message} </label>                     

  <div class="card-body">

                        <p class="h5 text-center py-4" style={{ color: 'green' }}>Edit Account: </p>

      <label class="grey-text font-weight-light text-center">OverDraft Limit :</label>
                        <input onBlur={_ => this.dispatchState({ over: this.state.over })}
                            onChange={e => this.setState({ over: e.target.value })} value={this.state.over} type="text" id="defaultFormCardNameEx" class="form-control"/>

                                <br/>

                        <label class="grey-text font-weight-light">Daily Wire Transfert </label>
                        <input onBlur={_ => this.dispatchState({ daily: this.state.daily })}
                            onChange={e => this.setState({ daily: e.target.value })} value={this.state.daily} type="email" id="defaultFormCardEmailEx" class="form-control"/>
                        <br />

                        <div class="text-center py-4 mt-3">
                            <button onClick={_ => this.dispatchState({ ButtonClicked: this.props.value })} class="btn btn-warning" type="">Send<i
                                                class="fa fa-paper-plane-o ml-2"></i></button>
                                        </div>
                                
                    </div>
</div>
            </div>
        </div> );
    }
}
 
export default Edit;