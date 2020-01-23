import './Comment.scss'
import React from 'react'

const Comment = (props) => {
    return (
        <div className='comment'>
            {props.comment.body}
        </div>
    )
}

export default Comment
