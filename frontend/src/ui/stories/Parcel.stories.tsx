import { createStory, createTemplate } from '@/utils/storybook';
import { Meta } from '@storybook/react';
import { Parcel } from '../components/Parcel';

export default {
    title: 'Components/Parcel',
    component: Parcel
} as Meta;

const Template = createTemplate(Parcel);

export const Default = createStory(Template, {
    parcel: {
        id: 'abcdefghijklmn',
        returnAddress: {
            name: 'John Doe',
            street: 'Kapellestraat',
            houseNumber: '9',
            postalCode: '9230',
            city: 'Wetteren',
            country: 'België'
        },
        deliveryAddress: {
            name: 'John Doe',
            street: 'Kapellestraat',
            houseNumber: '9',
            postalCode: '9230',
            city: 'Wetteren',
            country: 'België'
        },
        states: [
            {
                name: 'Created',
                description: 'The parcel has been created.',
                created: '2021/06/20'
            }
        ]
    }
});
