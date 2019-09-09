import React from 'react';
import renderer from 'react-test-renderer';
import { shallow, mount } from 'enzyme';
import Edit from './Edit';
import Account from './Account';

test(" Ui snapshot Render for Account", () => {
    const tree = renderer
        .create(<Account />)
        .toJSON();
    expect(tree).toMatchSnapshot();

});

test(" Ui snapshot Render for Edit", () => {
    const tree = renderer
        .create(<Edit />)
        .toJSON();
    expect(tree).toMatchSnapshot();

});

test('Account : Testing Container Render', () => {
    const component = shallow(<Account />);
    const wrapper = component.find('.container');
    expect(wrapper.length).toBeGreaterThan(0);

});

test('Edit : Testing popup Render', () => {
    const component = shallow(<Edit />);
    const wrapper = component.find('.popup');
    expect(wrapper.length).toBeGreaterThan(0);

});

test('Status Fonction : Testing true return ', () => {
    var res = Status(true);

    expect(res).toBe("table-danger");

});

test('Status Fonction : Testing false return ', () => {
    var res = Status(false);

    expect(res).toBe("table-success");

});

test('ErrorMessage Fonction : Testing empty string ', () => {
    var res = ErrorMessage("");

    expect(res).toBe(undefined);

});

test('ErrorMessage Fonction : Testing empty string ', () => {

    var node = <div class="alert alert-danger" role="alert">
        < p className="text-center" >
            Hello
            </p >
        </div >
    var res = ErrorMessage("Hello");

    expect(res).toEqual(node);

});

test('TotalValue : Testing Total prop Render', () => {

    const tree = renderer
        .create(<Edit value="sqd546-qsdq56-dsq89" />)
    var instance = tree.getInstance();
    expect(instance.props.value).toBe("sqd546-qsdq56-dsq89");

});

const Status = (bool) => {
    if (bool) return "table-danger";
    return "table-success";
};

const ErrorMessage = (message) => {
    if (message !== "") {
        return <div class="alert alert-danger" role="alert">
            <p className="text-center">{message}</p>
        </div>
    }
}


