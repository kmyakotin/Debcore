import * as React from 'react';
import {RouteComponentProps} from 'react-router';
import 'isomorphic-fetch';
import {NavLink} from "react-router-dom";
import {Party} from "./Party";

interface PartyList {
    parties: PartyName[];
    loading: boolean;
}

interface PartyName {
    name: string;
}

export class Parties extends React.Component<RouteComponentProps<{}>, PartyList> {
    constructor(props: any) {
        super(props);
        this.state = {parties: [], loading: true};
        this.handleClick = this.handleClick.bind(this);

        fetch('api/party/my')
            .then(res => console.log(res.json()));
        fetch('api/party/my')
            .then(response => response.json() as Promise<PartyName[]>)
            .then(data => {
                this.setState({parties: data, loading: false});
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderMyParties(this.state.parties);

        return <div>
            <h1>My parties</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>;
    }

    private renderMyParties(parties: PartyName[]) {
        return <div>
            <ul className='nav navbar-nav'>
                <li>
                    {parties.map(party =>
                        <NavLink to={`/party/${party.name}`} exact activeClassName='active'>
                            {party.name}
                        </NavLink>
                    )}</li>
            </ul>
        </div>;
    }

    private handleClick(name: any, e: any) {
        console.log('react rocks')
    }
}

