import { http } from "../../../http";
export const deletePublicationPhoto = async (publicationId, accessToken) => {
    const result = await http({
        path: `/publication/${publicationId}/photo`,
        method: "delete",
        accessToken,
    });
    if (result.ok) {
        return true;
    }
    else {
        return false;
    }
};
