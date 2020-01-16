import React from 'react'
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';

export const Categories = () => {

    const [category, setCategory] = React.useState('');

    return (
        <FormControl>
            <InputLabel>Category</InputLabel>
            <Select
                value={category}
                onChange={(e) => setCategory(e.target.value)}
            >
                <MenuItem value={10}>Ten</MenuItem>
                <MenuItem value={20}>Twenty</MenuItem>
                <MenuItem value={30}>Thirty</MenuItem>

                {/* {this.props.tags.map(x =>
                <Link key={x.id} to={`/news/category/${encodeURIComponent(x.name)}`}>{x.name}</Link>
            )} */}
            </Select>
        </FormControl >
    )
}

export default Categories
