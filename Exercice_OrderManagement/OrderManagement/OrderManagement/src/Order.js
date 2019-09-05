import React from 'react';
import dotnetify from 'dotnetify';

export default class Order extends React.Component {
    constructor(props) {
        super(props);
        this.vm = dotnetify.react.connect("ViewModel", this);
        this.state = {
            d: "", list: [], textMessage: "", TextBoxValue: '',
            SimpleDropDownValue: '',
            SimpleDropDownOptions: [],
            DropDownValue: '',
            DropDownOptions: [],
            SimpleDropDownValue2: '',
            SimpleDropDownOptions2: [],
            DropDownValue2: '',
            DropDownOptions2: [],
            Price: "",
            Qts: "",
            Assets: [],
            dict: {},
            Total: "",
            message1: "",
            message2: "",
            message3: ""

        };
        this.dispatchState = state => this.vm.$dispatch(state);

    }
    
   
    render() {

        return (
          
            <div class="container contact-form">
           
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <div class="card border-danger mb-3 m-2" style={{ width: '18rem' }}>
                                <div class="card-header">Total Value :</div>
                                <div class="card-body text-danger">
                                    <h5 class="card-title">{this.state.Total} $</h5>
                                </div>
                            </div>
    </div>
                        <div class="col">
    </div>
                        <div class="col">
                            <div class="card border-primary  mb-3 m-2" style={{ width: '20rem' }}>
                                <div class="card-header">Assets :</div>
                                <div class="card-body text-primary ">
                                    <ul>
                                        {Object.keys(this.state.dict).map((key, index) =>
                                            <li key={index}>{key} Quantite : {this.state.dict[key].Quantite} Price : {this.state.dict[key].price}</li>
                                        )}
                                    </ul>
                                </div>
                            </div>
    </div>
                    </div>
                </div>
               
                <form class="dd" method="post">
                    <h3>Send Order</h3>
                    <label style={{ color:'red' }}> {this.state.message3} </label>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="exampleFormControlTextarea6" style={{ color: 'green' }} className="m-2">Action : </label>

                                <select
                                    className="form-control"
                                    value={this.state.SimpleDropDownValue}
                                    onChange={e => this.dispatchState({ SimpleDropDownValue: e.target.value })}
                                >
                                    <option value="" disabled>
                                        Choose...
                  </option>
                                    {this.state.SimpleDropDownOptions.map((text, idx) => (
                                        <option key={idx} value={text}>
                                            {text}
                                        </option>
                                    ))}
                                </select>
                                <b>{this.state.SimpleDropDownResult}</b>
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlTextarea6" style={{ color: 'green' }}>Price : </label>
                                <input  onBlur={_ => this.dispatchState({ Price: this.state.Price })}
                                    onChange={e => this.setState({ Price: e.target.value })} value={this.state.Price} class="form-control" placeholder="Price" />
                            </div>
                            <label className="m-2" style={{ color: 'red' }}>{this.state.message1}</label>
                            <div class="form-group">
                                <label for="exampleFormControlTextarea6" style={{ color: 'green' }}>Asset : </label>

                                <select
                                    className="form-control"
                                    value={this.state.SimpleDropDownValue2}
                                    onChange={e => this.dispatchState({ SimpleDropDownValue2: e.target.value })}
                                >
                                    <option value="" disabled>
                                        Choose...
                  </option>
                                    {this.state.SimpleDropDownOptions2.map((text, idx) => (
                                        <option key={idx} value={text}>
                                            {text}
                                        </option>
                                    ))}
                                </select>
                                <b>{this.state.SimpleDropDownResult2}</b>
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlTextarea6" style={{ color: 'green' }}>Quantite : </label>

                                <input onBlur={_ => this.dispatchState({ Qts: this.state.Qts })}
                                    onChange={e => this.setState({ Qts: e.target.value })} value={this.state.Qts} class="form-control" placeholder="Quantite" />
                                <label className="m-2" style={{ color: 'red' }}>{this.state.message2}</label>
                            </div>
                            <div class="form-group">
                                <input type="button" onClick={_ => this.dispatchState({ ButtonClicked: true })} name="btnSubmit" class="btnContact" value="Send Order" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="form-group shadow-textarea">
                                    <label for="exampleFormControlTextarea6" style={{ color: 'green' }}>History : </label>
                                    <textarea class="form-control z-depth-1" id="exampleFormControlTextarea6" rows="3" placeholder="History." value={this.state.history}></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
    }


