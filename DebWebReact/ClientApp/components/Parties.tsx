import * as React from 'react';
import {RouteComponentProps} from 'react-router';
import 'isomorphic-fetch';

interface FetchParties {
    parties: Party[];
    loading: boolean;
}

interface Party {
    name: string;
}

export class Parties extends React.Component<RouteComponentProps<{}>, FetchParties> {
    constructor() {
        super();
        this.state = {parties: [], loading: true};

        fetch('party/my')
            .then(response => response.json() as Promise<Party[]>)
            .then(data => {
                this.setState({parties: data, loading: false});
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Parties.renderMyParties(this.state.parties);

        return <div>
            <h1>My parties</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>;
    }

    private static renderMyParties(parties: Party[]) {
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
                        <td>{party.name}</td>
                    </tr>
                )}
                </tbody>
            </table>
        </div>;
    }
}

