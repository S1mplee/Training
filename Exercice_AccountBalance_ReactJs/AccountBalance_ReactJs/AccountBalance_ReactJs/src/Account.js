import React, { Component } from 'react';
import dotnetify from 'dotnetify';
import Edit from './Edit';

class Account extends Component {
    constructor()
    {
        super();
        this.vm = dotnetify.react.connect("AccountViewModel", this);

        this.state = {
            counter: 0,
            tags: [{ name: "Ali", Age: "18" }, { name: "Mohamed", Age: "20" }, { name: "Fathi", Age: "20" }],
            age: 0,
            list: [],
            value: "",
            g1: "",
            g2: "",
            message: "",
            showPopup: false,
            ID:""
        };
        this.handleme = this.handleme.bind(this);
        this.styles = {
            color: "green"
        };

        this.dispatchState = state => {
            this.setState(state);
            this.vm.$dispatch(state);
        };
    }

    componentWillUnmount() {
        this.vm.$destroy();
    }

   
    
    ErrorMessage() {
        if (this.state.message !== "") {
            return <div class="alert alert-danger" role="alert">
                <p className="text-center"> {this.state.message} </p>
            </div>
        }
    }

    handleme () {
        window.open(Navbar, '_blank');
    };

    

    render() { 
        const togglePopup = (key) => {
            this.setState({
                showPopup: !this.state.showPopup
            });

            this.dispatchState({ ID: key });
        };

        const Status = (bool) => {
            if (bool) return "table-danger";
            return "table-success";
        };

        return (<div className="container">
            {this.ErrorMessage()}
            <div className="table-wrapper">
                <div className="table-title">
                    <div className="row">
                    <div class="col-sm-6">
						<h2>Manage <b>AccountBalance</b></h2>
					</div>
                        <div class="col-sm-6"  >
                            <div class="form-group col-sm-4 myclass">
                                <label for="focusedInput">Holder Name</label>
                                <input class="form-control" onBlur={_ => this.dispatchState({ name: this.state.name })}
                                    onChange={e => this.setState({ name: e.target.value })} value={this.state.name} class="form-control" id="focusedInput" type="text" />
                            </div>
                            
                          
                            <a href="/add/" onClick={_ => this.dispatchState({ clicked: true })} class="btn btn-success" data-toggle="modal"><i class="material-icons">&#xE147;</i> <span>Add New Account</span></a>
    
                   

					</div>
                </div>
            </div>
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                            <th>
                                Day
						</th>
                        <th>NameHolder</th>
                        <th>Balance</th>
						<th>Over draft Limit</th>
                        <th>Wire Transfert limit</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                    <tbody>
                        {this.state.list.map(acc => <tr className={Status(acc.blocked)}>
						<td>
                                <span>
                                    {acc.DailyWireTransfetAchieved}
							</span>
                            </td>
                            <td class="table-success" ><p>{acc.HolderName}</p></td>
                            <td>{acc.cash}</td>
                            <td>{acc.overdraftlimit}</td>
                            <td>{acc.wiretransfertlimit}</td>
                            <td>
                                <div className="container">
                                    <div className="row">
                                        <div className="col-xs-4">
                                            <button disabled={acc.blocked} onClick={a => this.dispatchState({ accountid: acc.Id })} className="btn btn-danger btn-sm m-1">WithDraw</button>
                                        </div>
                                        <div className="col-xs-4 text-center">
                                            <button className="btn btn-success  btn-sm m-1" onClick={e => this.dispatchState({ id: acc.Id })}>Depose</button>
                                        </div>
                                        <div className="col-xs-2 text-right m-1">
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">$</span>
                                                    <span class="input-group-text"></span>
                                                </div>
                                                <input style={{width : '70px'}} type="text" class="form-control" onBlur={_ => this.dispatchState({ value: this.state.value })}
                                                    onChange={e => this.setState({ value: e.target.value })} value={this.state.value} aria-label="Amount (to the nearest dollar)" />
                                                <button className="btn btn-info  btn-sm m-2" onClick={(e) => togglePopup(acc.Id, e)}>Edit </button>

                                                <button disabled={acc.blocked} onClick={e => this.dispatchState({ Tid: acc.Id })}  className="btn btn-warning  btn-sm m-2">Transf </button>
                                                
                                            </div>                                            
                                          
                                        </div>
                                    </div>
                                </div>
                            </td>
                    </tr> )}
                   
                        {this.state.showPopup ?
                            <Edit
                                text='Close Me'
                                closePopup={togglePopup}
                                value={this.state.ID}
                            />
                            : null
                        }
                </tbody>
            </table>
            </div>
           
           
    </div>
	  );
    }
   
}
 
export default Account;