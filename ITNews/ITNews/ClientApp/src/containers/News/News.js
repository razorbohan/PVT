import React, { Component } from 'react';
import './News.scss'
import NewsComponent from '../../components/News/News'

export class News extends Component {

    constructor(props) {
        super(props);

        this.state = {
            news: null
        }
    };

    async componentDidMount() {
        const reponse = await fetch(`/api/GetNews/${this.props.match.params.id}`);
        const news = await reponse.json();

        this.setState({
            news
        });
    }

    render() {
        const news = this.state.news;

        return (
            <NewsComponent
                news={news}
                isShort={false} />
        );
    }
}
