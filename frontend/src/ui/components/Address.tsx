import { AddressInfo } from '@/interfaces';
import { FC } from 'react';

export interface AddressProps {
    address: AddressInfo;
    title: string;
}

export const Address: FC<AddressProps> = props => {
    const { address, title } = props;
    return (
        <div className="c-address">
            <h2>{title}</h2>
            <dl>
                <dt>Name</dt>
                <dd>{address.name}</dd>
                <dt>Street</dt>
                <dd>
                    {address.street} {address.houseNumber}
                </dd>
                <dt>City</dt>
                <dd>
                    {address.postalCode} {address.city}
                </dd>
                <dt>Country</dt>
                <dd>{address.country}</dd>
            </dl>
        </div>
    );
};
