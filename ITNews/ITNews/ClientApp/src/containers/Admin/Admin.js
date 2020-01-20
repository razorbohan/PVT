import './Admin.scss'
import React, { Component } from 'react'
import List from '../../components/List/List';
import { Button, TextField } from '@material-ui/core';

class Admin extends Component {

    constructor(props) {
        super(props);

        this.state = {
            allTags: [],
            allCategories: []
        };
    }

    async componentDidMount() {
        await this.fetchTags();
        await this.fetchCategories();
    }

    async fetchCategories() {
        const reponse = await fetch('/api/GetCategories');
        const allCategories = await reponse.json();

        this.setState({ allCategories });
    }

    async fetchTags() {
        const reponse = await fetch('/api/GetTags');
        const allTags = await reponse.json();

        this.setState({ allTags });
    }

    handleChange = (e) => {
        const { name, value } = e.target
        this.setState({ [name]: value });
    }

    async handleCreate() {
        const { allTags, allCategories, ...formData } = this.state;

        console.log(formData);

        await fetch('/api/AddNews', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });

        
    }

    render() {
        return (
            <div className='admin'>
                <div className='title'>
                    <h2>Add news</h2>
                </div>

                <div className='name'>
                    <TextField
                        fullWidth={true}
                        name='Name'
                        label='Name'
                        onChange={this.handleChange}
                    />
                </div>
                <div className='category'>
                    <List
                        name='Category'
                        items={this.state.allCategories}
                        onChange={(category) => this.handleChange({ target: { name: 'Category', value: category } })} />
                </div>
                <div className='tags'>
                    <List
                        name='Tags'
                        items={this.state.allTags}
                        onChange={(tag) => this.handleChange({ target: { name: 'Tags', value: [tag] } })} />
                </div>
                <div className='short'>
                    <TextField
                        multiline
                        fullWidth={true}
                        variant='outlined'
                        name='ShortDescription'
                        label='Short description'
                        rows='2'
                        onChange={this.handleChange}
                    />
                </div>
                <div className='description'>
                    <TextField
                        multiline
                        fullWidth={true}
                        variant='outlined'
                        name='Description'
                        label='Description'
                        rows='10'
                        onChange={this.handleChange}
                    />
                </div>

                <Button
                    variant='contained'
                    color='primary'
                    onClick={() => this.handleCreate()}>
                    Create
                </Button>
            </div>
        )
    }
}

export default Admin
