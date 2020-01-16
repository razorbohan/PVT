import './Tags.scss'
import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import TagCloud from 'react-tag-cloud';
import randomColor from 'randomcolor';

export default class Tags extends Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        setInterval(() => {
            this.forceUpdate();
        }, 3000);
    }

    render() {
        return (//this.props.tags.length > 0 ?
            <div className='app-outer'>
                <div className='app-inner'>
                    <TagCloud
                        className='tag-cloud'
                        style={{
                            fontFamily: 'sans-serif',
                            //fontSize: () => Math.round(Math.random() * 20) + 16,
                            fontSize: 16,
                            fontWeight: 100,
                            color: () => randomColor({
                                hue: 'blue'
                            }),
                            padding: 5,
                        }}>

                        {this.props.tags.map(x =>
                            // <div key={x.id}>{x.name}</div>
                            <Link key={x.id} to={`/news/tag/${encodeURIComponent(x.name)}`}>{x.name}</Link>
                        )}
                    </TagCloud>
                </div>
            </div>
        )
    }
}