import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom'
import './News.scss'

const News = (props) => {
    const news = props.news;

    return !!news && (
        <div className='news'>
            {props.isShort
                ? <Link className='name' to={`/news/${news.id}`}>{news.name}</Link>
                : <p className='name' style={{ margin: 'auto' }}>{news.name}</p>}
            <div className='meta'>
                {/* <span className='category'>{news.category.name}</span> */}
                <Link className='category' to={`/news/category/${encodeURIComponent(news.category.name)}`}>{news.category.name}</Link>
                <div className='tags'>
                    {news.tags.map(x =>
                        // <span key={x.tag.id}>{x.tag.name}</span>
                        <Link key={x.tag.id} to={`/news/tag/${encodeURIComponent(x.tag.name)}`}>{x.tag.name}</Link>
                    )}
                </div>
            </div>

            {props.isShort
                ? <p className='short'>{news.shortDescription}</p>
                : <pre className='desc'>{news.description}</pre>}

            <p className='created'>{(new Date(news.created)).toLocaleString()}</p>
        </div>
    );
}

export default News
