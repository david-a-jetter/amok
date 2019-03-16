class ExchangeRatesResponse{
    constructor(baseCurrency, date, rates){
        
        this.baseCurrency = baseCurrency;
        this.date = date;

        this.rates = [{}]

        for(var rate in rates){
            this.rates.push({
                key: rate[0],
                value: rate[1]
            })
        }

        Object.freeze(this);
    }
}

export default ExchangeRatesResponse;