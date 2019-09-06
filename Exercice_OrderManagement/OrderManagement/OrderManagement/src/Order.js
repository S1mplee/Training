import React from 'react';
import dotnetify from 'dotnetify';
import TotalValue from './TotalValue';
import Asset from './Assets';

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

    componentWillUnmount() {
        this.vm.$destroy();
    }

    Clickme() {

    }
    
   
    render() {

        return (
          
            <div className="container contact-form">
           
                <div className="container">
                    <div className="row">
                      
                        <div className="col">
                            <TotalValue Total={this.state.Total} />
    </div>
                        <div className="col">
                            <div className="card border-primary  mb-3 m-2" style={{ width: '20rem' }}>
                                <div className="card-header">Assets :</div>
                                <Asset dict={this.state.dict} />
                            </div>
    </div>
                    </div>
                </div>
               
                <form className="dd" method="post">
                    <h3>Send Order</h3>
                    <label style={{ color:'red' }}> {this.state.message3} </label>

                    <div className="row">
                        <div className="col-md-6">
                            <div className="form-group">
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
                            <div className="form-group">
                                <label for="exampleFormControlTextarea6" style={{ color: 'green' }}>Price : </label>
                                <input  onBlur={_ => this.dispatchState({ Price: this.state.Price })}
                                    onChange={e => this.setState({ Price: e.target.value })} value={this.state.Price} className="form-control" placeholder="Price" />
                            </div>
                            <label className="m-2" style={{ color: 'red' }}>{this.state.message1}</label>
                            <div className="form-group">
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
                            <div className="form-group">
                                <label for="exampleFormControlTextarea6" style={{ color: 'green' }}>Quantite : </label>

                                <input onBlur={_ => this.dispatchState({ Qts: this.state.Qts })}
                                    onChange={e => this.setState({ Qts: e.target.value })} value={this.state.Qts} className="form-control" placeholder="Quantite" />
                                <label className="m-2" style={{ color: 'red' }}>{this.state.message2}</label>
                            </div>
                            <div className="form-group">
                                <input type="button" onClick={_ => this.dispatchState({ ButtonClicked: true })} name="btnSubmit" className="btnContact" value="Send Order" />
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="form-group">
                                <div className="form-group shadow-textarea">
                                    <label for="exampleFormControlTextarea6" style={{ color: 'green' }}>History : </label>
                                    <textarea className="form-control z-depth-1" id="exampleFormControlTextarea6" rows="3" placeholder="History." value={this.state.history}></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
    }


