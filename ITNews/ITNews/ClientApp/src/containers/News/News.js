import React, { Component } from 'react';
import './News.scss'
import NewsComponent from '../../components/News/News'
import Comment from '../../components/Comment/Comment'

class News extends Component {

    constructor(props) {
        super(props);

        this.state = {
            news: null,
            comments: []
        }
    };

    async componentDidMount() {
        await this.fetchNews();
        await this.fetchComments();
    }

    async fetchNews() {
        const reponse = await fetch(`/api/GetNews/${this.props.match.params.id}`);
        const news = await reponse.json();

        this.setState({ news });
    }

    async fetchComments() {
        const reponse = await fetch(`/api/GetNewsComments/${this.props.match.params.id}`);
        const comments = await reponse.json();

        this.setState({ comments });
    }

    render() {
        const news = this.state.news;

        return (
            <div className='news'>
                <NewsComponent
                    news={news}
                    isShort={false} />

                {this.state.comments.length > 0 ? this.state.comments.map(comment =>
                    <Comment
                        key={comment.id}
                        comment={comment} />
                ) : <p>No comments yet</p>}
            </div>
        );
    }
}

export default News
