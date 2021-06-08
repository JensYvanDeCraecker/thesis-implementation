import { ParcelStateInfo } from '@/interfaces';
import { format } from 'date-fns';
import { FC } from 'react';

export interface StateTableProps {
    states?: ParcelStateInfo[];
}

export const StateTable: FC<StateTableProps> = props => {
    const { states } = props;

    const getRows = () => states?.map(state => <StateTableRow key={state.created} state={state} />);

    return (
        <table className="c-table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Date</th>
                </tr>
            </thead>
            <tbody>{states && states.length > 0 ? getRows() : <EmptyTable />}</tbody>
        </table>
    );
};

const StateTableRow: FC<{ state: ParcelStateInfo }> = props => {
    const { state } = props;

    return (
        <tr>
            <td>{state.name}</td>
            <td>{state.description}</td>
            <td>{format(new Date(state.created), 'yyyy/MM/dd HH:mm:ss') }</td>
        </tr>
    );
};

const EmptyTable: FC = () => (
    <tr className="c-table--empty">
        <td colSpan={3}>No states found.</td>
    </tr>
);
