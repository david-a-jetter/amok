class ExchangeRatesResponse{
    constructor(baseCurrency, date, rates){
        
        this.baseCurrency = () => baseCurrency;
        this.date = () => date;

        this.rates = [{}]

        // for(var rate in rates){
        //     this.rates.push({
        //         key: rate[0],
        //         value: rate[1]
        //     })
        // }

        Object.keys(rates).forEach(rateKey =>{
            let rateValue = rates[rateKey];

            this.rates.push({
                key: rateKey, 
                value: rateValue});
        })

        Object.freeze(this);
    }
}

export default ExchangeRatesResponse;