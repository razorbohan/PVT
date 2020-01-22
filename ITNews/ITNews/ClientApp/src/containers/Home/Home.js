import './Home.scss'
import React, { Component } from 'react';
import Calendar from 'react-calendar'
import Button from '@material-ui/core/Button';
import News from '../../components/News/News'
import Tags from '../../components/Tags/Tags';
import List from '../../components/List/List';

class Home extends Component {

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
    const { category, tag, search, date } = this.props.match.params;

    let news;

    if (!!category) {
      const reponse = await fetch(`/api/GetNewsByCategory/${category}`);
      news = await reponse.json();
    } else if (!!tag) {
      const reponse = await fetch(`/api/GetNewsByTag/${tag}`);
      news = await reponse.json();
    } else if (!!date) {
      const reponse = await fetch(`/api/GetNewsByDate/${date}`);
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
        category, tag, search, date
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
    const { category, tag, search, date } = this.state.filter;

    if (category || tag || search || date) {
      return (
        <div className='filter'>
          <div className='filter-item'>
            {category ? <p>Category: '{decodeURIComponent(category)}'</p> : null}
            {tag ? <p>Tag: '{decodeURIComponent(tag)}'</p> : null}
            {search ? <p>Search text: '{decodeURIComponent(search)}'</p> : null}
            {date ? <p>Date: '{new Date(decodeURIComponent(date)).toLocaleDateString()}'</p> : null}
            <Button
              variant='outlined'
              color='primary'
              onClick={() => this.onReset()}>
              Reset
            </Button>
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
            <Button
              variant='outlined'
              color='primary'
              onClick={() => this.onSearch()}>
              Search
            </Button>
          </div>

          <List
            name='Categories'
            items={this.state.categories}
            onChange={(category) => this.props.history.push(`/news/category/${encodeURIComponent(category)}`)} />

          <div className='calendar'>
            <Calendar
              onChange={(date) => this.props.history.push(`/news/date/${encodeURIComponent(date.toISOString())}`)} />
          </div>

          <div className='tags-cloud'>
            <Tags tags={this.state.tags} />
          </div>
        </div>
      </div>
    );
  }
}

export default Home

//TODO: users editing page
//TODO: user profile page (photo, registration date)
//TODO: registration age (16+)
//TODO: comments, likes, rating

//TODO: news paging
//TODO: language changing (en, ru)
//TODO: night/day mode