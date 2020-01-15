import React, { Component } from 'react';
import { Link, withRouter } from 'react-router-dom'
import './Home.scss'
import News from '../../components/News/News'

export class Home extends Component {

  constructor(props) {
    super(props);

    this.state = {
      news: []
    };
  }

  async componentDidMount() {
    const { category, tag } = this.props.match.params;

    let news;

    if (!!category) {
      const reponse = await fetch(`/api/GetNewsByCategory/${category}`);
      news = await reponse.json();
    } else if (!!tag) {
      const reponse = await fetch(`/api/GetNewsByTag/${tag}`);
      news = await reponse.json();
    } else {
      const reponse = await fetch('/api/GetNews');
      news = await reponse.json();
    }

    console.log(news);

    this.setState({
      news
    });
  }

  render() {
    return (
      <div className='home'>
        {!!this.state.news ? this.state.news.map(news =>
          <News
            key={news.id}
            news={news}
            isShort={true} />
          // (<div className='news' key={news.id}>
          //   <Link className='name' to={`/news/${news.id}`}>{news.name}</Link>
          //   <div className='meta'>
          //     <span className='category'>{news.category.name}</span>
          //     <div className='tags'>
          //       {news.newsTags.map(x => <span key={x.tag.id}>{x.tag.name}</span>)}
          //     </div>
          //   </div>
          //   <p className='short'>{news.shortDescription}</p>
          //   <p className='created'>{(new Date(news.created)).toLocaleString()}</p>
          // </div>)
        ) : "No news found :("}
      </div>
    );
  }
}

//TODO: current filter, reset filter
//TODO: search bar
//TODO: date filtering (calendar)
//TODO: all categories & tags (on the right)
//TODO: news creation page
//TODO: user profile page (photo, registration date)
//TODO: SSO (Google, Facebook, VK) 
//TODO: users editing page

//TODO: registration age (16+)
//TODO: registration confirming (email)
//TODO: comments, likes, rating
//TODO: news paging
//TODO: language changing (en, ru)
//TODO: nihgt/day mode