import { createStory } from '@/utils/storybook';
import { Meta, Story } from '@storybook/react';
import { useState } from 'react';
import { Select, SelectProps } from '../components/Select';

export default {
    title: 'Components/Controls/Select',
    component: Select
} as Meta;

const Template: Story<SelectProps> = args => {
    const [value, setValue] = useState(args.value);
    const handleChange = (newValue: string) => {
        setValue(newValue);
        args.onChange && args.onChange(newValue);
    };
    return <Select {...args} value={value} onChange={handleChange} />;
};

export const Default = createStory(Template, {
    items: [
        {
            name: 'Created',
            value: '1'
        },
        {
            name: 'Sorting',
            value: '2'
        }
    ]
});

export const Disabled = createStory(Template, {
    disabled: true
});
