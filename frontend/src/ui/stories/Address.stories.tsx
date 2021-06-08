import { createStory, createTemplate } from '@/utils/storybook';
import { Meta } from '@storybook/react';
import { Address } from '../components/Address';

export default {
    title: 'Components/Address',
    component: Address
} as Meta;

const Template = createTemplate(Address);

export const Default = createStory(Template, {
    address: {
        name: 'John Doe',
        street: 'Kapellestraat',
        houseNumber: '9',
        postalCode: '9230',
        city: 'Wetteren',
        country: 'België'
    },
    title: 'Return address'
})