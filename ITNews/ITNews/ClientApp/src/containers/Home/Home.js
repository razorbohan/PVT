import React, { Component } from 'react';
import './Home.scss'
import News from '../../components/News/News'
import Tags from '../../components/Tags/Tags';
import Categories from '../../components/Categories/Categories';


export class Home extends Component {

  constructor(props) {
    super(props);

    this.state = {
      news: [],
      tags: [],
      categories: [],
      search: '',
      filter: {}
    };
  }

  async componentDidMount() {
    await this.fetchNews();
    await this.fetchTags();
    await this.fetchCategories();
  }

  componentDidUpdate(prevProps) {
    if (this.props.location !== prevProps.location) {
      this.fetchNews();
    }
  }

  async fetchCategories() {
    const reponse = await fetch('/api/GetCategories');
    const categories = await reponse.json();

    this.setState({ categories });
  }

  async fetchTags() {
    const reponse = await fetch('/api/GetTags');
    const tags = await reponse.json();

    this.setState({ tags });
  }

  async fetchNews() {
    const { category, tag, search } = this.props.match.params;

    let news;

    if (!!category) {
      const reponse = await fetch(`/api/GetNewsByCategory/${category}`);
      news = await reponse.json();
    } else if (!!tag) {
      const reponse = await fetch(`/api/GetNewsByTag/${tag}`);
      news = await reponse.json();
    } else if (!!search) {
      const reponse = await fetch(`/api/SearchNews/${search}`);
      news = await reponse.json();
    } else {
      const reponse = await fetch('/api/GetNews');
      news = await reponse.json();
    }

    this.setState({
      news,
      filter: {
        category, tag, search
      }
    }, () => {
      console.log(news);
      console.log(this.state.filter);
    });
  }

  onSearch() {
    if (!this.state.search)
      return;

    this.props.history.push(`/news/search/${encodeURIComponent(this.state.search)}`);
  }

  filter() {
    const { category, tag, search } = this.state.filter;

    if (category || tag || search) {
      return (
        <div className='filter'>
          <div className='filter-item'>
            {category ? <p>Category: '{decodeURIComponent(category)}'</p> : null}
            {tag ? <p>Tag: '{decodeURIComponent(tag)}'</p> : null}
            {search ? <p>Search text: '{decodeURIComponent(search)}'</p> : null}
            <button onClick={() => this.onReset()}>Reset</button>
          </div>
        </div>
      )
    }

    return null;
  }

  onReset() {
    this.props.history.push(`/news`);
  }

  render() {
    return (
      <div className='home'>
        <div className='feed'>
          {this.filter()}

          {this.state.news.length > 0 ? this.state.news.map(news =>
            <News
              key={news.id}
              news={news}
              isShort={true} />
          ) : <p>No news found :(</p>}
        </div>

        <div className='search-container'>
          <div className='search'>
            <input
              type='text'
              placeholder='Enter your search...'
              value={this.state.search}
              onChange={(e) => this.setState({ search: e.target.value })}
            />
            <button
              onClick={() => this.onSearch()}>
              Search
            </button>
          </div>

          <Categories categories={this.state.categories} />

          <div className='calendar'>
            Calendar
          </div>

          <div className='tags-cloud'>
            <Tags tags={this.state.tags} />
          </div>
        </div>
      </div>
    );
  }
}

//TODO: all categories (on the right)
//TODO: date filtering (calendar)
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