import { createStory, createTemplate } from '@/utils/storybook';
import { Meta } from '@storybook/react';
import { FindParcel } from '../components/FindParcel';

export default {
    title: 'Components/FindParcel',
    component: FindParcel
} as Meta;

const Template = createTemplate(FindParcel);

export const Default = createStory(Template);
