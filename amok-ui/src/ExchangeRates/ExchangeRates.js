import React, { Component } from 'react';
import axios from 'axios'
import ExchangeRatesResponse from './ExchangeRatesResponse.js';
import RatesDisplay from './RatesDisplay.js';

export default class ExchangeRates extends Component {
    constructor(props){
        super(props)

        this.state = { rates: {}}};

    async getCurrentRates(){

        var apiUrl = new URL('api/ExchangeRates', this.props.apiHost)
        var apiResponse = await axios.get(apiUrl);
        var apiData = apiResponse.data;

        this.setState(new ExchangeRatesResponse(
            apiData.baseCurrency,
            apiData.date,
            apiData.rates));
    }

    render() {
      return <div>
          <div>ApiHost: {this.props.apiHost}</div>
          <button className="Api-Button" onClick={() => this.getCurrentRates() }>Get Latest Rates</button>
          <RatesDisplay rates={this.state.rates} />
      </div>
    }
}