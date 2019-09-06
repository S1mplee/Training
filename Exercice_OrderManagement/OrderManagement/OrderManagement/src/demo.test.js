import React from 'react';
import Order from './Order';
import renderer from 'react-test-renderer';
import { shallow, mount } from 'enzyme';
import TotalValue from './TotalValue';
import Asset from './Assets';

test(" snapshot test", () => {
    const tree = renderer
        .create(<Order />)
        .toJSON();
    expect(tree).toMatchSnapshot();

});

test('Order : Testing Container Render', () => {
    const component = shallow(<Order />);
    const wrapper = component.find('.container');
    expect(wrapper.length).toBeGreaterThan(0);
 
});

test('Order : Testing Form Render', () => {
    const component = shallow(<Order />);
    const wrapper = component.find('.dd');
    expect(wrapper.length).toBeGreaterThan(0);

});

test('TotalValue : Testing Column Render', () => {
    const component = shallow(<TotalValue />);
    const wrapper = component.find('.col');
    expect(wrapper.length).toBeGreaterThan(0);

});

test('TotalValue : Testing Total prop Render', () => {
    const tree = renderer
        .create(<TotalValue Total='900'/>)
        .toJSON();
    expect(tree).toMatchSnapshot();

    const wrapper = mount(<TotalValue Total="900" />);
    const res = wrapper.find('.card-title');

    expect(res.text()).toBe("900 $");

});

test('TotalValue : Testing Card Render', () => {
    const component = shallow(<TotalValue />);
    const wrapper = component.find('.card-body');
    expect(wrapper.length).toBeGreaterThan(0);

});

test('TotalValue : Testing dict prop Render', () => {
    var dict1 = {};
    dict1["Asset1"] = { Quantite: "5", price: "100" };

    const tree = renderer
        .create(<Asset dict={dict1} />)
        .toJSON();
    expect(tree).toMatchSnapshot();

    const wrapper = mount(<Asset dict={dict1} />);

    const res = wrapper.find('li');

    expect(res.text()).toBe("Asset1 Quantite : 5 Price : 100");


});





