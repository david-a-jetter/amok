import React, { Component } from 'react';

export default class RatesDisplay extends Component{
    constructor(props){
        super(props)
        
        this.rates = props.rates;
        //this.rows = this.renderRates(props.rates);
    }

    renderRates = (rates) => {
        var rows = [];

        var i = 0;

        Object.keys(rates).forEach(key => rows.push(this.renderRow(key, rates[key], i++)));

        return rows;
    }

    renderRow = (key, value, index) => {
        let rowID = `row${index}`
        return (<tr key={index} id={rowID}> 
            <td>{key}</td> 
            <td>{value}</td> 
        </tr>)
    }

    render(){
        var rows = [];

        Object.keys(this.rates).forEach((rateKey, i) => {
            var rowID = `row${i}`

            var keyCellId = `key${i}`
            var keyCell = (<td key={keyCellId} id={keyCellId}>{rateKey}</td>)

            var valueCellId = `value${i}`
            var valueCell = (<td key={valueCellId} id={valueCellId}>{this.rates[rateKey]}</td>)

            rows.push(<tr key={i} id={rowID}>{keyCell} {valueCell}</tr>)
        });

        return (
        <table>
            <tbody>
                <tr>
                    <th>Currency</th>
                    <th>Exchange Rate</th>
                </tr>
                {rows.map(row => <div>{row}</div>)}
            </tbody>
        </table>)
    }
}