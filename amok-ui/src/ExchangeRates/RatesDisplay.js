import React, { Component } from 'react';
import ExchangeRatesResponse from './ExchangeRatesResponse.js';

export default class RatesDisplay extends Component{
    constructor(props){
        super(props);

        this.state = {rates: props.rates};
    }

    render(){
        return <table></table>
    }
}