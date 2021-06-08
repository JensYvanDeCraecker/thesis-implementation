import { createStory, createTemplate } from '@/utils/storybook';
import { Meta } from '@storybook/react';
import { StateTable } from '../components/StateTable';

export default {
    title: 'Components/StateTable',
    component: StateTable
} as Meta;

const Template = createTemplate(StateTable);

export const Default = createStory(Template, {
    states: [{ name: 'Created', description: 'Parcel has been created.', created: '2021/06/01' }]
});

export const Empty = createStory(Template);
