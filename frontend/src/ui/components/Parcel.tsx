import { ParcelInfo } from '@/interfaces';
import { FC } from 'react';
import { Address } from './Address';
import { StateTable } from './StateTable';

export interface ParcelProps {
    parcel?: ParcelInfo;
}

export const Parcel: FC<ParcelProps> = props => {
    const { parcel } = props;

    return parcel ? (
        <div className="c-parcel">
            <h1>Parcel: {parcel.id}</h1>
            <div className="c-parcel__addresses">
                <Address title="Return address" address={parcel.returnAddress} />
                <Address title="Delivery address" address={parcel.deliveryAddress} />
            </div>
            <h2>History</h2>
            <StateTable states={parcel.states} />
        </div>
    ) : (
        <div className="c-parcel c-parcel--empty"><span>Search your parcel using the search box. <br />If this message keeps appearing, check the parcel id for errors.</span></div>
    );
};
