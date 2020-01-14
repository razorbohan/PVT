import React, { Component } from 'react';
import { Link, withRouter} from 'react-router-dom'
import './Home.scss'

export class Home extends Component {

  constructor(props) {
    super(props);

    this.state = {
      news: []
    };
  }

  async componentDidMount() {
    const reponse = await fetch('/api/GetNews');
    const news = await reponse.json();

    console.log(news);
    console.log(new Date(news[0].created));

    var tt = `/api/GetNews/${news.id}`

    this.setState({
      news
    });
  }

  render() {
    return (
      <div className='home'>
        {this.state.news.map(news =>
          (<div className='news' key={news.id}>
            <Link to={`/api/GetNews/${news.id}`}>{news.name}</Link>
            <div className='meta'>
              <span className='category'>{news.category.name}</span>
              <div className='tags'>
                {news.newsTags.map(x => <span>{x.tag.name}</span>)}
              </div>
            </div>
            <p className='short'>{news.shortDescription}</p>
            <p className='created'>{(new Date(news.created)).toLocaleString()}</p>
          </div>)
        )}
      </div>
    );
  }
}
