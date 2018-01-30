import * as React from 'react';
import {PromptProps, RouteComponentProps} from 'react-router';
import 'isomorphic-fetch';
import * as ReactDOM from "react-dom";

interface IMember {
    name: string;
    buyProducts: IProduct[];
    consumeProducts: IProduct[];
    _id: string;
}

interface IProduct {
    name: string;
    price: number;
    id: string;
    participantsGuids: string[];
}

interface IPartyInfo {
    name: string;
    desctiption: string;
    partyId: string;
    participants: IMember[];
    products: IProduct[];
    loading: boolean;
}

interface IPartyId {
    alias: string
}

export class Product extends React.Component<IProduct, IProduct> {
    constructor(props: any) {
        super(props);
        this.state = props;
    }

    public render() {

        return <div>
            {console.log(this.props)}
            <span>{this.state.name} </span> 
            <span>{this.state.price} </span>
        </div>;
    }
}

export class Party extends React.Component<RouteComponentProps<IPartyId>, IPartyInfo> {
    constructor(props: any) {
        super(props);
        const alias = this.props.match.params.alias;
        this.state = {name: alias, loading: true, desctiption: "", participants: [], partyId: "", products: []};
        this.handleClick = this.handleClick.bind(this);

        fetch(`api/party/${alias}`)
            .then(res => res.json() as Promise<IPartyInfo>)
            .then(data => {
                this.setState(data);
                this.setState({loading: false});
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderParty(this.state);

        return <div>
            <h1>{this.state.name}</h1>
            {contents}
        </div>;
    }

    private renderParty(party: IPartyInfo) {
        return <div>
            <p>Render party {party.name}</p>
            <p>{party.partyId}</p>
            <div/>
            {party.products.map(p =>
                <span key={p.id}>{console.log(p.participantsGuids)}
                    <Product participantsGuids={p.participantsGuids} id={p.id} name={p.name} price={p.price}/></span>)
            }

        </div>;
    }

    private handleClick(name: any, e: any) {
        e.preventDefault();
        console.log(name);
    }
}


