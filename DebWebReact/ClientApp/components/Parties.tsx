import * as React from 'react';
import {RouteComponentProps} from 'react-router';
import 'isomorphic-fetch';

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
        
        fetch('party/my')
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
            <table className='table'>
                <thead>
                <tr>
                    <th>name</th>
                </tr>
                </thead>
                <tbody>
                {parties.map(party =>
                    <tr key={party.name}>
                        <td><a href="#" onClick={(e) => this.handleClick(party.name, e)}>
                            {party.name}</a></td>
                    </tr>
                )}
                </tbody>
            </table>
        </div>;
    }

    private handleClick(name: any, e: any) {
        e.preventDefault();
        console.log(name); console.log(this);
    }
}

