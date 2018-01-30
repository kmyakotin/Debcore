import * as React from 'react';
import {Route} from 'react-router-dom';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import {FetchData} from './components/FetchData';
import {Counter} from './components/Counter';
import {Parties} from './components/Parties';
import {Party} from "./components/Party";

export const routes = <Layout>
    <Route exact path='/' component={Home}/>
    <Route path='/counter' component={Counter}/>
    <Route path='/fetchdata' component={FetchData}/>
    <Route exact path='/party' component={Parties}/>
    <Route path='/party/:alias' component={Party}/>
</Layout>;
