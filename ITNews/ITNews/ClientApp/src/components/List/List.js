import React from 'react'
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';

export const List = (props) => {

    const [currentItem, setCurrentItem] = React.useState('');

    const onChange = (item) => {
        setCurrentItem(item);

        props.onChange && props.onChange(item);
    }

    return (
        <FormControl fullWidth={true}>
            <InputLabel>{props.name}</InputLabel>
            <Select
                autoWidth={true}
                value={currentItem}
                onChange={(e) => onChange(e.target.value)}
            >
                {props.items.map((x) =>
                    <MenuItem
                        key={x.id}
                        value={x.name}>
                        {x.name}
                    </MenuItem>)}
            </Select>
        </FormControl >
    )
}

export default List
